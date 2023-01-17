using Microsoft.AspNetCore.Hosting;
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
    public class ViewRendererService : IViewRendererService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewRendererService"/> class.
        /// </summary>
        /// <param name="viewEngine">The view engine.</param>
        /// <param name="tempDataProvider">The temporary data provider.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="pdfConverter">The PDF converter.</param>
        /// <param name="webHostEnvironment">The web host environment.</param>
        /// <param name="logger">The logger.</param>
        public ViewRendererService(
            IRazorViewEngine viewEngine,
            ITempDataProvider tempDataProvider,
            IServiceProvider serviceProvider,
            IServiceScopeFactory serviceScopeFactory,
            IConverter pdfConverter,
            IWebHostEnvironment webHostEnvironment,
            ILogger<ViewRendererService> logger)
        {
            _viewEngine = viewEngine;
            _tempDataProvider = tempDataProvider;
            _serviceProvider = serviceProvider;
            ServiceScopeFactory = serviceScopeFactory;
            PdfConverter = pdfConverter;
            WebHostEnvironment = webHostEnvironment;
            Logger = logger;
        }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public ILogger<ViewRendererService> Logger { get; }

        /// <summary>
        /// Gets the PDF converter.
        /// </summary>
        /// <value>The PDF converter.</value>
        public IConverter PdfConverter { get; }

        /// <summary>
        /// Gets the web host environment.
        /// </summary>
        /// <value>The web host environment.</value>
        public IWebHostEnvironment WebHostEnvironment { get; }

        /// <summary>
        /// The service provider
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// The temporary data provider
        /// </summary>
        private readonly ITempDataProvider _tempDataProvider;

        /// <summary>
        /// The view engine
        /// </summary>
        private readonly IRazorViewEngine _viewEngine;

        /// <summary>
        /// The service scope factory
        /// </summary>
        private readonly IServiceScopeFactory ServiceScopeFactory;

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
            if (string.IsNullOrEmpty(name))
                return Array.Empty<byte>();
            renderOptions ??= new RenderOptions { Orientation = Orientation.Landscape };
            using var Scope = _serviceProvider.CreateScope();
            var actionContext = GetActionContext(Scope);

            var viewEngineResult = _viewEngine.FindView(actionContext, name, false);
            if (viewEngineResult?.Success != true)
            {
                viewEngineResult = _viewEngine.GetView("~/Views/" + name + ".cshtml", "~/Views/" + name + ".cshtml", false);

                if (viewEngineResult?.Success != true)
                {
                    throw new InvalidOperationException($"Couldn't find view '{name}'");
                }
            }

            var view = viewEngineResult.View;
            if (view is null)
                return Array.Empty<byte>();

            using var output = new StringWriter();
            var viewContext = new ViewContext(
                actionContext,
                view,
                new ViewDataDictionary<TModel>(
                    metadataProvider: new EmptyModelMetadataProvider(),
                    modelState: new ModelStateDictionary())
                { Model = model },
                new TempDataDictionary(
                    actionContext.HttpContext,
                    _tempDataProvider),
                output,
                new HtmlHelperOptions());

            await view.RenderAsync(viewContext).ConfigureAwait(false);

            var ResultHTML = output.ToString();
            if (format == RenderFormat.PDF)
                return RenderPDF(ResultHTML, renderOptions);
            return Encoding.UTF8.GetBytes(ResultHTML);
        }

        /// <summary>
        /// Gets the action context.
        /// </summary>
        /// <returns></returns>
        private ActionContext GetActionContext(IServiceScope serviceScope)
        {
            var httpContext = new DefaultHttpContext
            {
                RequestServices = serviceScope.ServiceProvider
            };
            return new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
        }

        /// <summary>
        /// Renders the HTML in PDF format
        /// </summary>
        /// <param name="resultHTML">The result HTML.</param>
        /// <param name="renderOptions">The render options.</param>
        /// <returns>The PDF's bytes</returns>
        private byte[] RenderPDF(string resultHTML, RenderOptions renderOptions)
        {
            var TempOrientation = SimpleHtmlToPdf.Settings.Enums.Orientation.Landscape;
            if (renderOptions.Orientation == Orientation.Portrait)
                TempOrientation = SimpleHtmlToPdf.Settings.Enums.Orientation.Portrait;
            var doc = new HtmlToPdfDocument()
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

            return PdfConverter.Convert(doc);
        }
    }
}