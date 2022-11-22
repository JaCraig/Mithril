using Microsoft.AspNetCore.Authorization;
using Mithril.Core.Abstractions.Modules.BaseClasses;

namespace Mithril.Models
{
    /// <summary>
    /// Test module
    /// </summary>
    /// <seealso cref="ModuleBaseClass&lt;TestModule&gt;"/>
    public class TestModule : ModuleBaseClass<TestModule>
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
            return services?.Configure<AuthorizationOptions>(x => x.AddPolicy("Test", y => y.RequireAuthenticatedUser()));
        }
    }
}