using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mithril.Apm.Abstractions.Configuration;
using Mithril.Apm.Abstractions.Features;
using Mithril.Apm.Abstractions.Interfaces;
using Mithril.Apm.Abstractions.Services;
using Mithril.Apm.Default.Middleware;
using Mithril.Apm.Default.Services;
using Mithril.Core.Abstractions.Modules.BaseClasses;
using Mithril.Core.Abstractions.Modules.Interfaces;
using Mithril.Data.Abstractions.Services;
using System.Data;

namespace Mithril.Apm.Default
{
    /// <summary>
    /// Default APM Module
    /// </summary>
    /// <seealso cref="ModuleBaseClass&lt;DefaultApmModule&gt;"/>
    public class DefaultApmModule : ModuleBaseClass<DefaultApmModule>
    {
        /// <summary>
        /// Gets or sets the features.
        /// </summary>
        /// <value>The features.</value>
        public override IFeature[] Features { get; protected set; } = new IFeature[] { APMFeature.Instance };

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
            if (services is null || configuration is null)
                return services;
            return services.Configure<APMOptions>(configuration.GetSection("Mithril:APM"))
                           ?.AddAllSingleton<IMetricsCollector>()
                           ?.AddAllSingleton<IMetaDataCollector>()
                           ?.AddSingleton<IMetricsCollectorService, MetricsCollectorService>()
                           ?.AddAllSingleton<IMetricsReporter>()
                           ?.AddAllSingleton<IEventListener>()
                           ?.AddScoped<ApmMiddleware>();
        }

        /// <summary>
        /// Initializes the data.
        /// </summary>
        /// <param name="dataService">The data service.</param>
        /// <param name="services">The services for the application.</param>
        /// <returns>
        /// The async task.
        /// </returns>
        public override Task InitializeDataAsync(IDataService? dataService, IServiceProvider? services)
        {
            return dataService?.QueryDynamicAsync("DELETE FROM [RequestMetaData_];DELETE FROM [RequestMetric_];DELETE FROM [RequestTrace_];", CommandType.Text, "Default") ?? Task.CompletedTask;
        }
    }
}