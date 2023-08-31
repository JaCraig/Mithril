using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mithril.Core.Abstractions.Modules.BaseClasses;

namespace Mithril.Security.Windows
{
    /// <summary>
    /// Windows Authentication module
    /// </summary>
    /// <seealso cref="ModuleBaseClass{AuthenticationModule}"/>
    public class WindowsAuthenticationModule : ModuleBaseClass<WindowsAuthenticationModule>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsAuthenticationModule"/> class.
        /// </summary>
        public WindowsAuthenticationModule()
            : base("Windows Authentication Module", "Security", "Authentication", "Authorization")
        {
        }

        /// <summary>
        /// Configures the services for the module.
        /// </summary>
        /// <param name="services">The services collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="env"></param>
        public override IServiceCollection? ConfigureServices(IServiceCollection? services, IConfiguration? configuration, IHostEnvironment? env)
        {
            //Set up authentication so things get activated in IIS.
            _ = (services?.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
                     .AddNegotiate());

            return services;
        }
    }
}