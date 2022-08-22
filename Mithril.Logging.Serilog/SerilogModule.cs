using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mithril.Core.Abstractions.Modules.BaseClasses;
using Mithril.Core.Abstractions.Modules.Features;
using Mithril.Core.Abstractions.Modules.Interfaces;
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
        /// Initializes a new instance of the <see cref="SerilogModule"/> class.
        /// </summary>
        public SerilogModule()
        {
            Features = new IFeature[] { new LoggingFeature() };
        }

        /// <summary>
        /// Configures the application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="environment">The environment.</param>
        public override IApplicationBuilder? ConfigureApplication(IApplicationBuilder? app, IConfiguration? configuration, IHostEnvironment? environment)
        {
            return app?.UseMiddleware<LoggingMiddleware>();
        }

        /// <summary>
        /// Configures the host settings.
        /// </summary>
        /// <param name="host">The host.</param>
        public override IHostBuilder? ConfigureHostSettings(IHostBuilder? host, IConfiguration? configuration, IHostEnvironment? environment)
        {
            return host?.UseSerilog();
        }

        /// <summary>
        /// Configures the logging settings.
        /// </summary>
        /// <param name="logging">The logging.</param>
        public override ILoggingBuilder? ConfigureLoggingSettings(ILoggingBuilder? logging, IConfiguration? configuration, IHostEnvironment? environment)
        {
            return logging?.AddSerilog();
        }

        /// <summary>
        /// Configures the services for the module.
        /// </summary>
        /// <param name="services">The services collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        public override IServiceCollection? ConfigureServices(IServiceCollection? services, IConfiguration? configuration, IHostEnvironment? environment)
        {
            string RootPath = environment?.ContentRootPath ?? ".";
            var Assembly = System.Reflection.Assembly.GetEntryAssembly();
            var AssemblyName = Assembly?.GetName().Name ?? "";
            var SerilogConfig = configuration?.GetSection("Serilog");
            if (SerilogConfig?.Exists() == true)
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