﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mithril.Mvc.Abstractions;
using Mithril.Mvc.Abstractions.Enums;
using Mithril.Mvc.Abstractions.Services;
using SimpleHtmlToPdf;
using SimpleHtmlToPdf.Interfaces;
using SimpleHtmlToPdf.Settings;
using System.Text;

namespace Mithril.Mvc.Services
{
    /// <summary>
    /// View renderer service
    /// </summary>
    /// <seealso cref="IViewRendererService"/>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ViewRendererService"/> class.
    /// </remarks>
    /// <param name="viewEngine">The view engine.</param>
    /// <param name="tempDataProvider">The temporary data provider.</param>
    /// <param name="serviceProvider">The service provider.</param>
    /// <param name="pdfConverter">The PDF converter.</param>
    /// <param name="webHostEnvironment">The web host environment.</param>
    /// <param name="logger">The logger.</param>
    public class ViewRendererService(
        IRazorViewEngine? viewEngine,
        ITempDataProvider? tempDataProvider,
        IServiceProvider? serviceProvider,
        IConverter? pdfConverter,
        IWebHostEnvironment? webHostEnvironment,
        ILogger<ViewRendererService>? logger) : IViewRendererService
    {
        /// <summary>
        /// The service provider
        /// </summary>
        private readonly IServiceProvider? _ServiceProvider = serviceProvider;

        /// <summary>
        /// The temporary data provider
        /// </summary>
        private readonly ITempDataProvider? _TempDataProvider = tempDataProvider;

        /// <summary>
        /// The view engine
        /// </summary>
        private readonly IRazorViewEngine? _ViewEngine = viewEngine;

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public ILogger<ViewRendererService>? Logger { get; } = logger;

        /// <summary>
        /// Gets the PDF converter.
        /// </summary>
        /// <value>The PDF converter.</value>
        public IConverter? PdfConverter { get; } = pdfConverter;

        /// <summary>
        /// Gets the web host environment.
        /// </summary>
        /// <value>The web host environment.</value>
        public IWebHostEnvironment? WebHostEnvironment { get; } = webHostEnvironment;

        /// <summary>
        /// Renders the specified name.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="name">The name.</param>
        /// <param name="model">The model.</param>
        /// <param name="renderOptions">The render options.</param>
        /// <param name="format">The format.</param>
        /// <returns>The rendered view.</returns>
        /// <exception cref="InvalidOperationException">Couldn't find view '{name}'</exception>
        public async Task<byte[]> RenderAsync<TModel>(string? name, TModel model, RenderOptions? renderOptions = default, RenderFormat format = RenderFormat.HTML)
        {
            if (string.IsNullOrEmpty(name) || _ServiceProvider is null || _ViewEngine is null || _TempDataProvider is null)
                return [];
            renderOptions ??= new RenderOptions { Orientation = Orientation.Landscape };
            using IServiceScope Scope = _ServiceProvider.CreateScope();
            ActionContext ActionContext = GetActionContext(Scope);

            Microsoft.AspNetCore.Mvc.ViewEngines.ViewEngineResult? ViewEngineResult = _ViewEngine.FindView(ActionContext, name, false);
            if (ViewEngineResult?.Success != true)
            {
                ViewEngineResult = _ViewEngine.GetView("~/Views/" + name + ".cshtml", "~/Views/" + name + ".cshtml", false);

                if (ViewEngineResult?.Success != true)
                {
                    throw new InvalidOperationException($"Couldn't find view '{name}'");
                }
            }

            Microsoft.AspNetCore.Mvc.ViewEngines.IView? View = ViewEngineResult.View;
            if (View is null)
                return [];

            using var Output = new StringWriter();
            var ViewContext = new ViewContext(
                ActionContext,
                View,
                new ViewDataDictionary<TModel>(
                    metadataProvider: new EmptyModelMetadataProvider(),
                    modelState: new ModelStateDictionary())
                { Model = model },
                new TempDataDictionary(
                    ActionContext.HttpContext,
                    _TempDataProvider),
                Output,
                new HtmlHelperOptions());

            await View.RenderAsync(ViewContext).ConfigureAwait(false);

            var ResultHTML = Output.ToString();
            return format == RenderFormat.PDF ? RenderPDF(ResultHTML, renderOptions) : Encoding.UTF8.GetBytes(ResultHTML);
        }

        /// <summary>
        /// Gets the action context.
        /// </summary>
        /// <returns></returns>
        private static ActionContext GetActionContext(IServiceScope serviceScope)
        {
            var HttpContext = new DefaultHttpContext
            {
                RequestServices = serviceScope.ServiceProvider
            };
            return new ActionContext(HttpContext, new RouteData(), new ActionDescriptor());
        }

        /// <summary>
        /// Renders the HTML in PDF format
        /// </summary>
        /// <param name="resultHTML">The result HTML.</param>
        /// <param name="renderOptions">The render options.</param>
        /// <returns>The PDF's bytes</returns>
        private byte[] RenderPDF(string resultHTML, RenderOptions renderOptions)
        {
            SimpleHtmlToPdf.Settings.Enums.Orientation TempOrientation = SimpleHtmlToPdf.Settings.Enums.Orientation.Landscape;
            if (renderOptions.Orientation == Orientation.Portrait)
                TempOrientation = SimpleHtmlToPdf.Settings.Enums.Orientation.Portrait;
            var Doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = SimpleHtmlToPdf.Settings.Enums.ColorMode.Color,
                    Orientation = TempOrientation,
                    PaperSize = SimpleHtmlToPdf.UnmanagedHandler.PaperKind.A4Plus,
                },
                Objects = {
                    new ObjectSettings()
                    {
                        HtmlContent = resultHTML,
                        WebSettings = { DefaultEncoding = "utf-8" },
                    },
                }
            };

            return PdfConverter?.Convert(Doc) ?? [];
        }
    }
}