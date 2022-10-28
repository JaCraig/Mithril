﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Mithril.Core.Abstractions.Configuration;
using Mithril.Core.Abstractions.Extensions;
using Mithril.Core.Abstractions.Modules.BaseClasses;
using Mithril.Core.Abstractions.Modules.Features;
using Mithril.Core.Abstractions.Modules.Interfaces;
using Mithril.Core.Extensions;
using Mithril.Core.Middleware;
using Mithril.Data.Abstractions.Services;

namespace Mithril.Core.Modules
{
    /// <summary>
    /// Mithril module
    /// </summary>
    /// <seealso cref="ModuleBaseClass&lt;MithrilModule&gt;"/>
    public class MithrilModule : ModuleBaseClass<MithrilModule>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MithrilModule"/> class.
        /// </summary>
        public MithrilModule()
            : base(
                name: "Mithril Core Module",
                category: "Core",
                tags: new[] { "Email", "Content", "Media", "Navigation", "Search", "Workflow", "Logging" })
        {
        }

        /// <summary>
        /// Gets the features.
        /// </summary>
        /// <value>The features.</value>
        public override IFeature[] Features { get; protected set; } = new IFeature[]
        {
            new ContentFeature(),
            new EmailFeature(),
            new IndexingFeature(),
            new LoggingFeature(),
            new MediaFeature(),
            new NavigationFeature(),
            new WorkflowFeature()
        };

        /// <summary>
        /// Gets the order that they are initialized in.
        /// </summary>
        /// <value>The order that they are initialized in.</value>
        public override int Order { get; protected set; } = int.MaxValue;

        /// <summary>
        /// Configures the application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        public override IApplicationBuilder? ConfigureApplication(IApplicationBuilder? app, IConfiguration? configuration, IHostEnvironment? environment)
        {
            if (app is null || environment is null || configuration is null)
                return app;

            var Settings = configuration.GetSystemConfig();

            // Sets up static HTTP context
            app.UseStaticHttpContext();

            // Setup CSP middleware.
            app = app.UseMiddleware<CSPMiddleware>();

            // Setup XFrame middleware.
            app = app.UseMiddleware<XFrameOptionsMiddleware>();

            // Setup static files.
            app = SetupStaticFiles(app, configuration, environment);

            if (Settings?.Compression?.DynamicCompression == true)
            {
                // Use response compression.
                app = app.UseResponseCompression();
            }

            // Setup exception pages
            return app.When(environment.IsDevelopment(), builder => builder.UseDeveloperExceptionPage())
                      .When(!environment.IsDevelopment(), builder => builder.UseExceptionHandler("/Home/Error"));
        }

        /// <summary>
        /// Configures the MVC.
        /// </summary>
        /// <param name="mvcBuilder">The MVC builder.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        public override IMvcBuilder? ConfigureMVC(IMvcBuilder? mvcBuilder, IConfiguration? configuration, IHostEnvironment? environment)
        {
            return mvcBuilder?.AddCspMediaType();
        }

        /// <summary>
        /// Configures the routes.
        /// </summary>
        /// <param name="endpoints">The endpoints.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        public override IEndpointRouteBuilder? ConfigureRoutes(IEndpointRouteBuilder? endpoints, IConfiguration? configuration, IHostEnvironment? environment)
        {
            endpoints?.MapAreaControllerRoute("Admin_Route", "Admin", "Admin/{controller}/{action}/{id?}");
            endpoints?.MapDefaultControllerRoute();
            return endpoints;
        }

        /// <summary>
        /// Configures the services for the module.
        /// </summary>
        /// <param name="services">The services collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        public override IServiceCollection? ConfigureServices(IServiceCollection? services, IConfiguration? configuration, IHostEnvironment? environment)
        {
            //Memory cache
            services = services.AddMemoryCache();

            //Static HTTP context accessor services
            services = services.AddStaticHttpContextAccessor();

            if (configuration is null)
                return services;

            // Set up config.
            services = services.Configure<MithrilConfig>(configuration.GetSection("Mithril"));

            var Settings = configuration.GetSystemConfig();
            if (Settings?.Compression?.DynamicCompression == true)
            {
                // Add compression.
                services = services.AddResponseCompression(options => options.EnableForHttps = Settings.Compression.AllowHttps);
            }
            return services;
        }

        /// <summary>
        /// Initializes the data.
        /// </summary>
        /// <returns>The async task.</returns>
        public override Task InitializeDataAsync(IDataService dataService)
        {
            return Task.CompletedTask;
            /*var FeaturesFound = Feature.All();

            var ModuleFeatures = Modules.SelectMany(x => x.Features);

            foreach (var ModuleFeature in ModuleFeatures)
            {
                if (!FeaturesFound.Any(x => x.Identifier == ModuleFeature.Id))
                {
                    var TempFeature = new Feature(ModuleFeature.Name ?? ModuleFeature.Id, ModuleFeature.Id, ModuleFeature.Category)
                    {
                        Description = ModuleFeature.Description,
                    };
                    await TempFeature.SaveAsync().ConfigureAwait(false);
                }
            }
            foreach (var ModuleFeature in FeaturesFound)
            {
                if (!ModuleFeatures.Any(x => x.Id == ModuleFeature.Identifier))
                {
                    ModuleFeature.Delete(false);
                }
            }
            FeaturesFound = Feature.All();
            foreach (var ModuleFeature in ModuleFeatures)
            {
                var TempFeature = FeaturesFound.FirstOrDefault(x => x.Identifier == ModuleFeature.Id);
                if (TempFeature is null)
                    continue;

                await TempFeature.SaveAsync().ConfigureAwait(false);
            }*/
        }

        /// <summary>
        /// Setups the extension mappings.
        /// </summary>
        /// <param name="Config">The configuration.</param>
        /// <param name="provider">The provider.</param>
        private static void SetupMimeTypes(MithrilConfig? Config, FileExtensionContentTypeProvider provider)
        {
            if (Config?.MimeTypes is null)
                return;
            foreach (var Value in Config.MimeTypes)
            {
                if (string.IsNullOrWhiteSpace(Value?.Extension) || string.IsNullOrWhiteSpace(Value?.MimeType))
                    continue;
                provider.Mappings[Value.Extension] = Value.MimeType;
            }
        }

        /// <summary>
        /// Setups the static files.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        private static IApplicationBuilder SetupStaticFiles(IApplicationBuilder app, IConfiguration? configuration, IHostEnvironment? environment)
        {
            MithrilConfig? Config = configuration.GetSystemConfig();

            var provider = new FileExtensionContentTypeProvider();
            SetupMimeTypes(Config, provider);
            if (environment.IsDevelopment())
            {
                return app.UseStaticFiles(new StaticFileOptions
                {
                    ContentTypeProvider = provider,
                });
            }
            var MaxAge = Config?.StaticFiles?.CacheControlMaxAge <= 0 ? 31557600 : Config?.StaticFiles?.CacheControlMaxAge;
            return app.UseStaticFiles(new StaticFileOptions
            {
                ContentTypeProvider = provider,
                OnPrepareResponse = ctx => ctx.Context.Response.Headers[HeaderNames.CacheControl] = "public,max-age=" + MaxAge
            });
        }
    }
}