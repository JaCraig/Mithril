using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;
using Mithril.Communication.Email.HealthChecks;
using Mithril.Communication.Email.Models;
using Mithril.Core.Abstractions.Extensions;
using Mithril.Core.Abstractions.Modules.BaseClasses;
using Mithril.Data.Abstractions.Services;

namespace Mithril.Communication.Email
{
    /// <summary>
    /// Email module
    /// </summary>
    /// <seealso cref="ModuleBaseClass&lt;EmailModule&gt;"/>
    public class EmailModule : ModuleBaseClass<EmailModule>
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
            return services.Configure<HealthCheckServiceOptions>(options => options.Registrations.Add(new HealthCheckRegistration("Smtp", new SMTPHealthCheck(), null, new string[] { "Smtp" }, new TimeSpan(0, 0, Timeout))));
        }

        /// <summary>
        /// Initializes the data.
        /// </summary>
        /// <param name="dataService">Data service</param>
        /// <param name="services">The services for the application.</param>
        /// <returns>The async task.</returns>
        public override Task InitializeDataAsync(IDataService? dataService, IServiceProvider? services)
        {
            SMTPHealthCheck.DataService = dataService;
            SMTPHealthCheck.FeatureManager = services?.GetService<IFeatureManager>();
            return EmailSettings.LoadOrCreateAsync(dataService);
        }
    }
}