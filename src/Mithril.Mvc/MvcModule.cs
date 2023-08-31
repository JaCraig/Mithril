using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mithril.Core.Abstractions.Modules.BaseClasses;
using Mithril.Mvc.Abstractions.Services;
using Mithril.Mvc.Services;

namespace Mithril.Mvc
{
    /// <summary>
    /// MVC Module
    /// </summary>
    /// <seealso cref="ModuleBaseClass&lt;MvcModule&gt;"/>
    public class MvcModule : ModuleBaseClass<MvcModule>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MvcModule"/> class.
        /// </summary>
        public MvcModule()
            : base("MVC Module", "Core", "MVC")
        {
        }

        /// <summary>
        /// Configures the services for the module.
        /// </summary>
        /// <param name="services">The services collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>Services</returns>
        public override IServiceCollection? ConfigureServices(IServiceCollection? services, IConfiguration? configuration, IHostEnvironment? environment) => services?.AddSingleton<IViewRendererService, ViewRendererService>();
    }
}