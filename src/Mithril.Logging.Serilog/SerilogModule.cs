using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mithril.Core.Abstractions.Modules.BaseClasses;
using Mithril.Core.Abstractions.Modules.Interfaces;
using Mithril.Logging.Features;
using Mithril.Logging.Serilog.Middleware;
using Serilog;
using Serilog.Enrichers;

namespace Mithril.Logging.Serilog
{
    /// <summary>
    /// Serilog specific logging added to Mithril
    /// </summary>
    /// <seealso cref="ModuleBaseClass&lt;SerilogModule&gt;"/>
    public class SerilogModule : ModuleBaseClass<SerilogModule>
    {
        /// <summary>
        /// Gets the features.
        /// </summary>
        /// <value>The features.</value>
        public override IFeature[] Features => new IFeature[] { LoggingFeature.Instance };

        /// <summary>
        /// Configures the application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>Application builder</returns>
        public override IApplicationBuilder? ConfigureApplication(IApplicationBuilder? app, IConfiguration? configuration, IHostEnvironment? environment) => app?.UseMiddleware<LoggingMiddleware>();

        /// <summary>
        /// Configures the host settings.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>Host builder</returns>
        public override IHostBuilder? ConfigureHostSettings(IHostBuilder? host, IConfiguration? configuration, IHostEnvironment? environment) => host?.UseSerilog();

        /// <summary>
        /// Configures the logging settings.
        /// </summary>
        /// <param name="logging">The logging.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>Logging builder</returns>
        public override ILoggingBuilder? ConfigureLoggingSettings(ILoggingBuilder? logging, IConfiguration? configuration, IHostEnvironment? environment) => logging?.AddSerilog();

        /// <summary>
        /// Configures the services for the module.
        /// </summary>
        /// <param name="services">The services collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>Services</returns>
        public override IServiceCollection? ConfigureServices(IServiceCollection? services, IConfiguration? configuration, IHostEnvironment? environment)
        {
            var RootPath = environment?.ContentRootPath ?? ".";
            var Assembly = System.Reflection.Assembly.GetEntryAssembly();
            var AssemblyName = Assembly?.GetName().Name ?? "";
            IConfigurationSection? SerilogConfig = configuration?.GetSection("Serilog");
            if (SerilogConfig?.Exists() == true && configuration is not null)
            {
                try
                {
                    Log.Logger = new LoggerConfiguration()
                                    .ReadFrom.Configuration(configuration)
                                    .Enrich.FromLogContext()
                                    .Enrich.With<MachineNameEnricher>()
                                    .Enrich.With<EnvironmentNameEnricher>()
                                    .Enrich.WithProperty("Application", AssemblyName)
                                    .Enrich.WithProperty("ApplicationVersion", Assembly?.GetName().Version?.ToString() ?? "")
                                    .CreateLogger();
                    return services;
                }
                catch { }
            }

            Log.Logger = new LoggerConfiguration()
                            .MinimumLevel
                            .Debug()
                            .Enrich.FromLogContext()
                            .Enrich.With<MachineNameEnricher>()
                            .Enrich.With<EnvironmentNameEnricher>()
                            .Enrich.WithProperty("Application", AssemblyName)
                            .Enrich.WithProperty("ApplicationVersion", Assembly?.GetName().Version?.ToString() ?? "")
                            .WriteTo
                                .File(RootPath + "/Logs/log-.txt", outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] [{UserName}] {Message}{NewLine}{Exception}", rollingInterval: RollingInterval.Day)
                            .WriteTo
                                .Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] [{UserName}] {Message}{NewLine}{Exception}")
                            .CreateLogger();
            return services;
        }
    }
}