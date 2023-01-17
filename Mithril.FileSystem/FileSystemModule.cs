using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Mithril.Core.Abstractions.Extensions;
using Mithril.Core.Abstractions.Modules.BaseClasses;
using Mithril.FileSystem.Abstractions.Interfaces;
using Mithril.FileSystem.Abstractions.Services;
using Mithril.FileSystem.HealthChecks;
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
            if (services is null)
                return services;
            var Timeout = configuration?.GetSystemConfig()?.HealthChecks?.DefaultTimeout ?? 3;
            return services.Configure<HealthCheckServiceOptions>(options => options.Registrations.Add(new HealthCheckRegistration("Disk", new DiskSpaceHealthCheck(), null, new string[] { "Disk" }, new TimeSpan(0, 0, Timeout))))
                .AddSingleton<IFileSystemService, FileSystemService>()
                .AddAllSingleton<IPathConverter>();
        }
    }
}