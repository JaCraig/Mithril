using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mithril.API.Abstractions.Configuration;
using Mithril.Core.Abstractions.Modules.BaseClasses;

namespace Mithril.API.Abstractions.Modules
{
    /// <summary>
    /// API Module to register config information.
    /// </summary>
    /// <seealso cref="ModuleBaseClass&lt;APIModule&gt;"/>
    public class APIModule : ModuleBaseClass<APIModule>
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
            if (services is null || configuration is null)
                return services;
            // Set up config.
            return services.Configure<APIOptions>(configuration.GetSection("Mithril:API"));
        }
    }
}