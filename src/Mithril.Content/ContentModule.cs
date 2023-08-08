using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mithril.Content.Abstractions.Interfaces;
using Mithril.Content.Abstractions.Services;
using Mithril.Content.Services;
using Mithril.Core.Abstractions.Modules.BaseClasses;

namespace Mithril.Content
{
    /// <summary>
    /// Content module
    /// </summary>
    /// <seealso cref="ModuleBaseClass&lt;ContentModule&gt;" />
    public class ContentModule : ModuleBaseClass<ContentModule>
    {
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
            return services?.AddAllSingleton<IComponentDefinition>()
                           ?.AddSingleton<IComponentService, ComponentService>();
        }
    }
}