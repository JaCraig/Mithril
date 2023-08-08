using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mithril.Core.Abstractions.Modules.BaseClasses;
using Mithril.Core.Abstractions.Modules.Interfaces;
using Mithril.Navigation.Abstractions.Services;
using Mithril.Navigation.Features;
using Mithril.Navigation.Services;

namespace Mithril.Navigation
{
    /// <summary>
    /// Navigation module
    /// </summary>
    /// <seealso cref="ModuleBaseClass&lt;NavigationModule&gt;" />
    public class NavigationModule : ModuleBaseClass<NavigationModule>
    {
        /// <summary>
        /// Gets the features.
        /// </summary>
        /// <value>
        /// The features.
        /// </value>
        public override IFeature[] Features { get; protected set; } = new IFeature[] { NavigationFeature.Instance };

        /// <summary>
        /// Configures the services for the module.
        /// </summary>
        /// <param name="services">The services collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>
        /// Services
        /// </returns>
        public override IServiceCollection? ConfigureServices(IServiceCollection? services, IConfiguration? configuration, IHostEnvironment? environment)
        {
            return services?.AddSingleton<IMenuService, MenuService>();
        }
    }
}