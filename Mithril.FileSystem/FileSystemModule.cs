using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mithril.Core.Abstractions.Modules.BaseClasses;
using Mithril.FileSystem.Abstractions.Interfaces;
using Mithril.FileSystem.Abstractions.Services;
using Mithril.FileSystem.Services;

namespace Mithril.FileSystem
{
    /// <summary>
    /// File system module
    /// </summary>
    /// <seealso cref="ModuleBaseClass&lt;FileSystemModule&gt;"/>
    public class FileSystemModule : ModuleBaseClass<FileSystemModule>
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
            return services?.AddSingleton<IFileSystemService, FileSystemService>()
                .AddAllSingleton<IPathConverter>();
        }
    }
}