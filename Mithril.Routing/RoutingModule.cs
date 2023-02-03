using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mithril.Core.Abstractions.Modules.BaseClasses;
using Mithril.Routing.Abstractions.Services;
using Mithril.Routing.Services;

namespace Mithril.Routing
{
    /// <summary>
    /// Routing module
    /// </summary>
    /// <seealso cref="Mithril.Core.Abstractions.Modules.BaseClasses.ModuleBaseClass&lt;Mithril.Routing.RoutingModule&gt;"/>
    public class RoutingModule : ModuleBaseClass<RoutingModule>
    {
        /// <summary>
        /// Configures the services for the module.
        /// </summary>
        /// <param name="services">The services collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>Services</returns>
        public override IServiceCollection? ConfigureServices(IServiceCollection? services, IConfiguration? configuration, IHostEnvironment? environment)
        {
            return services?.AddSingleton<IRouteService, RouteService>();
        }
    }
}