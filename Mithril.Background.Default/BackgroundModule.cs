using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mithril.Background.Abstractions.Interfaces;
using Mithril.Background.Abstractions.Services;
using Mithril.Background.Default.HostedServices;
using Mithril.Background.Default.Services;
using Mithril.Core.Abstractions.Modules.BaseClasses;

namespace Mithril.Background.Default
{
    /// <summary>
    /// Background module
    /// </summary>
    /// <seealso cref="ModuleBaseClass&lt;BackgroundModule&gt;" />
    public class BackgroundModule : ModuleBaseClass<BackgroundModule>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BackgroundModule"/> class.
        /// </summary>
        public BackgroundModule()
        {
        }

        /// <summary>
        /// Configures the services for the module.
        /// </summary>
        /// <param name="services">The services collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        public override IServiceCollection? ConfigureServices(IServiceCollection? services, IConfiguration? configuration, IHostEnvironment? environment)
        {
            return services
                ?.AddAllTransient<IBackgroundTask>()
                ?.AddAllTransient<IScheduledTask>()
                ?.AddSingleton<IBackgroundTaskService, BackgroundTaskService>()
                ?.AddHostedService<BackgroundTasksHostedService>();
        }
    }
}