using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Mithril.Core.Abstractions.Extensions;
using Mithril.Core.Abstractions.Modules.BaseClasses;
using Mithril.Data.Abstractions.BaseClasses;
using Mithril.Data.Abstractions.Enums;
using Mithril.Data.Abstractions.Services;
using Mithril.Data.HealthCheck;
using Mithril.Data.Models.General;
using Mithril.Data.Services;

namespace Mithril.Data
{
    /// <summary>
    /// Data module
    /// </summary>
    /// <seealso cref="ModuleBaseClass&lt;DataModule&gt;"/>
    public class DataModule : ModuleBaseClass<DataModule>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataModule"/> class.
        /// </summary>
        public DataModule()
            : base("Data Access Module", "Data", "Data")
        {
        }

        /// <summary>
        /// Gets the order that they are initialized in.
        /// </summary>
        /// <value>The order that they are initialized in.</value>
        public override int Order { get; protected set; } = int.MinValue;

        /// <summary>
        /// Configures the services for the module.
        /// </summary>
        /// <param name="services">The services collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        public override IServiceCollection? ConfigureServices(IServiceCollection? services, IConfiguration? configuration, IHostEnvironment? environment)
        {
            if (services is null)
                return services;
            var Timeout = configuration?.GetSystemConfig()?.HealthChecks?.DefaultTimeout ?? 3;
            services.Configure<HealthCheckServiceOptions>(options => options.Registrations.Add(new HealthCheckRegistration("Database", new SqlConnectionHealthCheck(configuration), null, new string[] { "Database" }, new TimeSpan(0, 0, Timeout))));
            return services.AddTransient<IDataService, DataService>();
        }

        /// <summary>
        /// Initializes the data.
        /// </summary>
        /// <param name="dataService"></param>
        /// <returns>The async task.</returns>
        public override async Task InitializeDataAsync(IDataService dataService)
        {
            await SetupLookUpTypesAsync(dataService).ConfigureAwait(false);
            await SetupLookUpsAsync(dataService, ContactInfoType.GetContactInfoTypes(), LookUpTypeEnum.ContactInfoType).ConfigureAwait(false);
        }

        /// <summary>
        /// Setups the look ups asynchronously.
        /// </summary>
        /// <typeparam name="TLookUpClass">The type of the look up class.</typeparam>
        /// <param name="dataService">The data service.</param>
        /// <param name="lookUps">The look ups.</param>
        /// <param name="lookUpType">Type of the look up.</param>
        /// <returns>Async task.</returns>
        private static Task SetupLookUpsAsync<TLookUpClass>(IDataService dataService, IEnumerable<TLookUpClass> lookUps, LookUpTypeEnum lookUpType)
            where TLookUpClass : LookUpEnumBaseClass<TLookUpClass>, new()
        {
            if (dataService is null)
                return Task.CompletedTask;
            var Tasks = new List<Task>();
            foreach (var TempType in lookUps)
            {
                Tasks.Add(LookUp.LoadOrCreateAsync(TempType, lookUpType, TempType?.Icon ?? "", dataService));
            }
            return Task.WhenAll(Tasks);
        }

        /// <summary>
        /// Setups the look ups asynchronously.
        /// </summary>
        /// <param name="dataService">The data service.</param>
        /// <returns>The async task.</returns>
        private static Task SetupLookUpTypesAsync(IDataService dataService)
        {
            if (dataService is null)
                return Task.CompletedTask;
            List<Task> Tasks = new List<Task>();
            foreach (var TempType in LookUpTypeEnum.GetLookUpTypes())
            {
                Tasks.Add(LookUpType.LoadOrCreateAsync(TempType, TempType?.Description ?? "", dataService));
            }
            return Task.WhenAll(Tasks);
        }
    }
}