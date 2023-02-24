using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mithril.Core.Abstractions.Modules.BaseClasses;
using Mithril.Routing.Abstractions.Services;
using Mithril.Routing.Services;
using Mithril.Routing.Transformers;

namespace Mithril.Routing
{
    /// <summary>
    /// Routing module
    /// </summary>
    /// <seealso cref="ModuleBaseClass&lt;RoutingModule&gt;"/>
    public class RoutingModule : ModuleBaseClass<RoutingModule>
    {
        /// <summary>
        /// Configures the routes.
        /// </summary>
        /// <param name="endpoints">The endpoints.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>Endpoint route builder</returns>
        public override IEndpointRouteBuilder? ConfigureRoutes(IEndpointRouteBuilder? endpoints, IConfiguration? configuration, IHostEnvironment? environment)
        {
            // Add generic slug endpoint
            endpoints?.MapDynamicControllerRoute<RouteTransformer>("{**slug}");
            return endpoints;
        }

        /// <summary>
        /// Configures the services for the module.
        /// </summary>
        /// <param name="services">The services collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>Services</returns>
        public override IServiceCollection? ConfigureServices(IServiceCollection? services, IConfiguration? configuration, IHostEnvironment? environment)
        {
            // Add route service
            return services?.AddSingleton<IRouteService, RouteService>()
                            // Add route transformer to catch/forward items properly
                            .AddSingleton<RouteTransformer>();
        }
    }
}