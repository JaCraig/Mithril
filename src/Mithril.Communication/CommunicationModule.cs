using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mithril.Communication.Abstractions.Interfaces;
using Mithril.Communication.Abstractions.Services;
using Mithril.Communication.Services;
using Mithril.Core.Abstractions.Modules.BaseClasses;

namespace Mithril.Communication
{
    /// <summary>
    /// Communication module
    /// </summary>
    /// <seealso cref="ModuleBaseClass&lt;CommunicationModule&gt;"/>
    public class CommunicationModule : ModuleBaseClass<CommunicationModule>
    {
        /// <summary>
        /// Configures the services for the module.
        /// </summary>
        /// <param name="services">The services collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        public override IServiceCollection? ConfigureServices(IServiceCollection? services, IConfiguration? configuration, IHostEnvironment? environment)
        {
            return services?.AddAllSingleton<IChannel>()
                           ?.AddSingleton<ICommunicationService, CommunicationService>();
        }
    }
}