using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
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
using Mithril.Core.Abstractions.Services;
using Mithril.Core.Abstractions.Services.Options;
using Mithril.Core.Extensions;
using Mithril.Core.Middleware;
using Mithril.Core.Services;

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
                tags: new[] { "Email", "Content", "Media", "Navigation", "Search", "Workflow" })
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

            MithrilConfig? Settings = configuration.GetSystemConfig();

            // Sets up static HTTP context
            app = app.UseStaticHttpContext();

            // Setup CSP middleware.
            app = app?.UseMiddleware<CSPMiddleware>();

            // Setup XFrame middleware.
            app = app?.UseMiddleware<XFrameOptionsMiddleware>();

            // Setup IP Filter middleware for default policy.
            app = app?.UseMiddleware<IPFilterMiddleware>();

            if (!string.IsNullOrEmpty(Settings?.Security?.DefaultCorsPolicy))
                app = app?.UseCors(Settings?.Security?.DefaultCorsPolicy);

            // Setup response caching.
            app = app?.UseResponseCaching();

            // Setup static files.
            app = SetupStaticFiles(app, configuration, environment);

            if (Settings?.Compression?.DynamicCompression == true)
            {
                // Use response compression.
                app = app?.UseResponseCompression();
            }

            // Setup exception pages
            app = app.When(environment.IsDevelopment(), builder => builder?.UseDeveloperExceptionPage())
                      .When(!environment.IsDevelopment(), builder => builder?.UseExceptionHandler("/Home/Error"));
            if (Settings?.Security?.RequireHttps ?? false)
            {
                // Require HTTPS
                app = app?.UseHttpsRedirection()?.UseHsts();
            }
            return app;
        }

        /// <summary>
        /// Configures the routes.
        /// </summary>
        /// <param name="endpoints">The endpoints.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        public override IEndpointRouteBuilder? ConfigureRoutes(IEndpointRouteBuilder? endpoints, IConfiguration? configuration, IHostEnvironment? environment)
        {
            if (!(endpoints?.IsSetup() ?? false))
                return endpoints;
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
            if (services is null)
                return services;

            //Static HTTP context accessor services
            services = services.AddStaticHttpContextAccessor();

            if (configuration is null)
                return services;

            var Config = configuration.GetSystemConfig();

            // Set up config.
            services = services.Configure<MithrilConfig>(configuration.GetSection("Mithril"));

            // Set up default response caching profile
            services = services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new ResponseCacheAttribute { CacheProfileName = "Default" });
                options.CacheProfiles.Add("Default", new CacheProfile()
                {
                    Location = ResponseCacheLocation.Any,
                    NoStore = false,
                    Duration = (Config?.StaticFiles?.CacheControlMaxAge ?? 0) <= 0 ? 31557600 : (int)(Config?.StaticFiles?.CacheControlMaxAge ?? 0)
                });
            });

            // Set up response caching
            services = services.AddResponseCaching();

            MithrilConfig? Settings = configuration.GetSystemConfig();
            if (Settings?.Compression?.DynamicCompression == true)
            {
                // Add compression.
                services = services.AddResponseCompression(options => options.EnableForHttps = Settings.Compression.AllowHttps);
            }

            if (!string.IsNullOrEmpty(Settings?.Security?.DefaultCorsPolicy))
            {
                // Set up CORS
                services.AddCors();
            }

            // Set up IP filtering services.
            services.AddSingleton<IIPFilterService, IPFilterService>();
            services.AddOptions<IPFilterOptions>();

            // Add mithril setup flag
            return services.AddSingleton<MithrilSetup>();
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
            foreach (Mime Value in Config.MimeTypes)
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
        private static IApplicationBuilder? SetupStaticFiles(IApplicationBuilder? app, IConfiguration? configuration, IHostEnvironment? environment)
        {
            if (app is null) return null;

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