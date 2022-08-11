using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mithril.Core.Abstractions.Modules.BaseClasses;
using Mithril.Core.Abstractions.Services;
using Mithril.Security.Windows.Services;

namespace Mithril.Security.Windows
{
    /// <summary>
    /// Authentication module
    /// </summary>
    /// <seealso cref="ModuleBaseClass{AuthenticationModule}"/>
    public class AuthenticationModule : ModuleBaseClass<AuthenticationModule>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationModule"/> class.
        /// </summary>
        public AuthenticationModule()
            : base("Windows Authentication Module", "Security", "Authentication", "Authorization")
        {
        }

        /// <summary>
        /// Configures the application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        public override void ConfigureApplication(IApplicationBuilder app, IConfiguration configuration, IHostEnvironment environment)
        {
            // Activate authentication
            app.UseAuthentication();
        }

        /// <summary>
        /// Configures the services for the module.
        /// </summary>
        /// <param name="services">The services collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="env"></param>
        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
        {
            //Set up authentication so things get activated in IIS.
            services.AddAuthentication(IISServerDefaults.AuthenticationScheme);

            // Add the security service.
            services.AddSingleton<ISecurityService, SecurityService>();

            // Add the claims transformer
            services.AddScoped<IClaimsTransformation, UserClaimsTransformer>();
        }

        /// <summary>
        /// Initializes the data.
        /// </summary>
        /// <returns>The async task.</returns>
        public override Task InitializeDataAsync(IDataService dataService)
        {
            var UserService = new SecurityService(dataService);
            if (UserService is null) return Task.CompletedTask;
            UserService.LoadSystemAccount();
            UserService.LoadAnonymousUserAccount();
            return Task.CompletedTask;
        }
    }
}