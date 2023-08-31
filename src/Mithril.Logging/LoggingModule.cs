using BigBook;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mithril.Core.Abstractions.Modules.BaseClasses;
using Mithril.Core.Abstractions.Modules.Interfaces;
using Mithril.Logging.Features;

namespace Mithril.Logging
{
    /// <summary>
    /// Module that adds a command handler that accepts specific logging added to Mithril
    /// </summary>
    /// <seealso cref="ModuleBaseClass&lt;SerilogModule&gt;"/>
    public class LoggingModule : ModuleBaseClass<LoggingModule>
    {
        /// <summary>
        /// Gets the features.
        /// </summary>
        /// <value>The features.</value>
        public override IFeature[] Features => new IFeature[] { LoggingFeature.Instance };

        /// <summary>
        /// Gets the order that they are initialized in.
        /// </summary>
        /// <value>The order that they are initialized in.</value>
        public override int Order => int.MinValue;

        /// <summary>
        /// Configures the services for the module.
        /// </summary>
        /// <param name="services">The services collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>Services</returns>
        public override IServiceCollection? ConfigureServices(IServiceCollection? services, IConfiguration? configuration, IHostEnvironment? environment)
        {
            return services?.Configure<MvcOptions>(options =>
            {
                _ = options.InputFormatters
                        .OfType<SystemTextJsonInputFormatter>()
                        .ForEach(x => x.SupportedMediaTypes.Add("application/csp-report"));
            });
        }
    }
}