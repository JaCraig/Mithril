using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mithril.Core.Abstractions.Modules.BaseClasses;

namespace Mithril.Caching.InMemory
{
    /// <summary>
    /// In-Memory caching module
    /// </summary>
    /// <seealso cref="ModuleBaseClass&lt;InMemoryCachingModule&gt;"/>
    public class InMemoryCachingModule : ModuleBaseClass<InMemoryCachingModule>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryCachingModule"/> class.
        /// </summary>
        public InMemoryCachingModule()
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
            //Memory cache
            return services?.AddMemoryCacheHoard();
        }
    }
}