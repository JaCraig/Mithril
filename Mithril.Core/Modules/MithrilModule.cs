using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Mithril.Core.Abstractions.Configuration;
using Mithril.Core.Abstractions.Modules.BaseClasses;

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
            SetupStaticFiles(app, configuration, environment);
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
        /// Setups the static files.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        private static void SetupStaticFiles(IApplicationBuilder app, IConfiguration configuration, IHostEnvironment environment)
        {
            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".webmanifest"] = "application/manifest+json";
            if (environment.IsDevelopment())
            {
                app.UseStaticFiles(new StaticFileOptions
                {
                    ContentTypeProvider = provider,
                });
                return;
            }
            var Config = configuration.GetSection("Mithril").Get<MithrilConfig>();
            var MaxAge = Config?.StaticFiles.CacheControlMaxAge <= 0 ? 31557600 : Config?.StaticFiles.CacheControlMaxAge;
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