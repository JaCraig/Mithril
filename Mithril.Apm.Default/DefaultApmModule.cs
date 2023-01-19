using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mithril.Apm.Abstractions.Interfaces;
using Mithril.Apm.Abstractions.Services;
using Mithril.Apm.Default.HostedServices;
using Mithril.Apm.Default.Middleware;
using Mithril.Apm.Default.Services;
using Mithril.Core.Abstractions.Modules.BaseClasses;

namespace Mithril.Apm.Default
{
    /// <summary>
    /// Default APM Module
    /// </summary>
    /// <seealso cref="ModuleBaseClass&lt;DefaultApmModule&gt;"/>
    public class DefaultApmModule : ModuleBaseClass<DefaultApmModule>
    {
        /// <summary>
        /// Configures the application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>Application builder</returns>
        public override IApplicationBuilder? ConfigureApplication(IApplicationBuilder? app, IConfiguration? configuration, IHostEnvironment? environment)
        {
            return app?.UseMiddleware<ApmMiddleware>();
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
            return services?.AddAllSingleton<IMetricsCollector>()
                           ?.AddAllSingleton<ITraceDataCollector>()
                           ?.AddSingleton<IMetricsCollectorService, MetricsCollectorService>()
                           ?.AddAllSingleton<IMetricsReporter>()
                           ?.AddScoped<ApmMiddleware>()
                           ?.AddHostedService<MetricsReporterHostedService>();
        }
    }
}