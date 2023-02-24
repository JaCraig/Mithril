using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;
using Mithril.Core.Abstractions.Modules.BaseClasses;
using Mithril.Core.Abstractions.Modules.Interfaces;
using Mithril.Data.Abstractions.Services;
using Mithril.Features.Services;

namespace Mithril.Features
{
    /// <summary>
    /// Feature module
    /// </summary>
    /// <seealso cref="ModuleBaseClass&lt;FeatureModule&gt;"/>
    public class FeatureModule : ModuleBaseClass<FeatureModule>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureModule"/> class.
        /// </summary>
        public FeatureModule()
            : base("Feature Module", "Core", "Features")
        {
        }

        /// <summary>
        /// Gets the order that they are initialized in.
        /// </summary>
        /// <value>The order that they are initialized in.</value>
        public override int Order => int.MinValue;

        /// <summary>
        /// Configures the services for the module.
        /// </summary>
        /// <param name="services">The services collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>Services</returns>
        public override IServiceCollection? ConfigureServices(IServiceCollection? services, IConfiguration? configuration, IHostEnvironment? environment)
        {
            services?.AddFeatureManagement()
                .AddSessionManager<DatabaseSessionManager>();
            return services;
        }

        /// <summary>
        /// Initializes the data.
        /// </summary>
        /// <param name="dataService">The data service</param>
        /// <param name="services">The services for the application.</param>
        public override async Task InitializeDataAsync(IDataService? dataService, IServiceProvider? services)
        {
            if (services?.GetService<IModule>() is null)
                return;
            var Modules = services?.GetServices<IModule>();
            if (Modules is null)
                return;

            foreach (var Feature in Modules.SelectMany(x => x.Features).Distinct())
            {
                var TempFeature = await Models.Feature.LoadOrCreateAsync(Feature.Name, Feature.Category, dataService, null).ConfigureAwait(false);
                TempFeature.Category = Feature.Category;
                TempFeature.Description = Feature.Description;
                await TempFeature.SaveAsync(dataService, null).ConfigureAwait(false);
            }
        }
    }
}