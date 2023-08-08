using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mithril.Admin.Abstractions.Interfaces;
using Mithril.Admin.Abstractions.Services;
using Mithril.Admin.Services;
using Mithril.Core.Abstractions.Modules.BaseClasses;

namespace Mithril.Admin
{
    /// <summary>
    /// Admin module
    /// </summary>
    /// <seealso cref="ModuleBaseClass&lt;AdminModule&gt;"/>
    public class AdminModule : ModuleBaseClass<AdminModule>
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
            return services?.AddAllTransient<IEditor>()
                ?.AddAllTransient<IMetadataBuilder>()
                ?.AddSingleton<IEditorService, EditorService>()
                ?.AddSingleton<IEntityMetadataService, EntityMetadataService>();
        }
    }
}