using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Mithril.Core.Abstractions.Modules.BaseClasses;
using Mithril.Core.Abstractions.Services.Options;

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
            // Set IP filtering policies
            services = services?.Configure<IPFilterOptions>(options =>
            {
                options?.AddPolicy("AdminSection")?.SetWhiteList("127.0.0.1;::1");
                options?.AddDefaultPolicy()?.SetBlackList("10.0.0.1");
            });
            //Set up CORS
            services = services?.Configure<CorsOptions>(options =>
            {
                options.AddDefaultPolicy(x => x.AllowCredentials().WithOrigins("https://www.google.com"));
                options.AddPolicy("DefaultPolicy", x => x.AllowAnyOrigin());
            });
            //Set up authorization policies.
            return services?.Configure<AuthorizationOptions>(x =>
            {
                x.AddPolicy("AdminOnly", y => y.RequireAuthenticatedUser().RequireClaim("Role", "Admin2"));
                x.AddPolicy("Test", y => y.RequireAuthenticatedUser());
            });
        }
    }
}