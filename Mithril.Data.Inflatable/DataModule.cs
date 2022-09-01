using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mithril.Core.Abstractions.Modules.BaseClasses;
using Mithril.Core.Abstractions.Services;
using Mithril.Data.Inflatable.Services;

namespace Mithril.Data.Inflatable
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
            return services?.AddTransient<IDataService, DataService>();
        }
    }
}