using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Mithril.Core.Abstractions.Configuration;
using Mithril.Core.Abstractions.Modules.BaseClasses;
using Mithril.Core.Extensions;
using Mithril.Core.Middleware;

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
        {
            Order = int.MaxValue;
        }

        /// <summary>
        /// Configures the application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        public override void ConfigureApplication(IApplicationBuilder app, IConfiguration configuration, IHostEnvironment environment)
        {
            if (app is null || environment is null || configuration is null)
                return;

            var Settings = GetSystemConfig(configuration);

            // Setup CSP middleware.
            app.UseMiddleware<CSPMiddleware>();

            // Setup XFrame middleware.
            app.UseMiddleware<XFrameOptionsMiddleware>();

            // Setup static files.
            SetupStaticFiles(app, configuration, environment);

            if (Settings?.Compression?.DynamicCompression == true)
            {
                // Use response compression.
                app.UseResponseCompression();
            }

            // Setup exception pages
            app.When(environment.IsDevelopment(), builder => builder.UseDeveloperExceptionPage())
               .When(!environment.IsDevelopment(), builder => builder.UseExceptionHandler("/Home/Error"));
        }

        /// <summary>
        /// Configures the MVC.
        /// </summary>
        /// <param name="mvcBuilder">The MVC builder.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        public override void ConfigureMVC(IMvcBuilder? mvcBuilder, IConfiguration configuration, IHostEnvironment environment)
        {
            mvcBuilder?.AddCspMediaType();
        }

        /// <summary>
        /// Configures the routes.
        /// </summary>
        /// <param name="endpoints">The endpoints.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        public override void ConfigureRoutes(IEndpointRouteBuilder endpoints, IConfiguration configuration, IHostEnvironment environment)
        {
            endpoints?.MapDefaultControllerRoute();
        }

        /// <summary>
        /// Configures the services for the module.
        /// </summary>
        /// <param name="services">The services collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
        {
            // Set up config.
            services.Configure<MithrilConfig>(configuration.GetSection("Mithril"));

            var Settings = GetSystemConfig(configuration);
            if (Settings?.Compression?.DynamicCompression == true)
            {
                // Add compression.
                services.AddResponseCompression(options =>
                {
                    options.EnableForHttps = Settings.Compression.AllowHttps;
                });
            }
        }

        /// <summary>
        /// Initializes the data.
        /// </summary>
        /// <returns>The async task.</returns>
        public override Task InitializeDataAsync()
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
        /// Gets the system configuration from the IConfiguration object.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The system configuration.</returns>
        private static MithrilConfig GetSystemConfig(IConfiguration configuration) => configuration.GetSection("Mithril").Get<MithrilConfig>();

        /// <summary>
        /// Setups the extension mappings.
        /// </summary>
        /// <param name="Config">The configuration.</param>
        /// <param name="provider">The provider.</param>
        private static void SetupMimeTypes(MithrilConfig? Config, FileExtensionContentTypeProvider provider)
        {
            if (Config?.MimeTypes is null)
                return;
            foreach (var Value in Config.MimeTypes.Where(x => !string.IsNullOrWhiteSpace(x.Extension) && !string.IsNullOrWhiteSpace(x.MimeType)))
            {
                provider.Mappings[Value.Extension] = Value.MimeType;
            }
        }

        /// <summary>
        /// Setups the static files.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        private static void SetupStaticFiles(IApplicationBuilder app, IConfiguration configuration, IHostEnvironment environment)
        {
            MithrilConfig? Config = GetSystemConfig(configuration);

            var provider = new FileExtensionContentTypeProvider();
            SetupMimeTypes(Config, provider);
            if (environment.IsDevelopment())
            {
                app.UseStaticFiles(new StaticFileOptions
                {
                    ContentTypeProvider = provider,
                });
                return;
            }
            var MaxAge = Config?.StaticFiles?.CacheControlMaxAge <= 0 ? 31557600 : Config?.StaticFiles?.CacheControlMaxAge;
            app.UseStaticFiles(new StaticFileOptions
            {
                ContentTypeProvider = provider,
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers[HeaderNames.CacheControl] = "public,max-age=" + MaxAge;
                }
            });
        }
    }
}