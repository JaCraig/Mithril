﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mithril.Core.Abstractions.Modules.BaseClasses;
using Mithril.Data.Abstractions.Services;
using Mithril.Security.Abstractions;
using Mithril.Security.Abstractions.Enums;
using Mithril.Security.Abstractions.Services;
using Mithril.Security.Services;

namespace Mithril.Security
{
    /// <summary>
    /// Windows Authentication module
    /// </summary>
    /// <seealso cref="ModuleBaseClass{AuthenticationModule}"/>
    public class AuthenticationModule : ModuleBaseClass<AuthenticationModule>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationModule"/> class.
        /// </summary>
        public AuthenticationModule()
            : base("Authentication Module", "Security", "Authentication", "Authorization")
        {
        }

        /// <summary>
        /// Gets the order that they are initialized in.
        /// </summary>
        /// <value>The order that they are initialized in.</value>
        public override int Order => int.MinValue;

        /// <summary>
        /// Configures the application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        public override IApplicationBuilder? ConfigureApplication(IApplicationBuilder? app, IConfiguration? configuration, IHostEnvironment? environment)
        {
            if (app?.ApplicationServices.GetService(typeof(IAuthorizationService)) is null)
                return app;
            // Activate authentication/authorization
            return app?.UseAuthentication().UseAuthorization();
        }

        /// <summary>
        /// Configures the services for the module.
        /// </summary>
        /// <param name="services">The services collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="env"></param>
        public override IServiceCollection? ConfigureServices(IServiceCollection? services, IConfiguration? configuration, IHostEnvironment? env)
        {
            // Add authorization.
            _ = (services?.AddAuthorization(options => options.FallbackPolicy = options.DefaultPolicy));

            //Set up default authorization policies.
            _ = (services?.Configure<AuthorizationOptions>(x => x.AddPolicy(SystemPermissions.AdminOnly, y => y.RequireAuthenticatedUser().RequireClaim("Role", "Admin"))));

            // Add the security services.
            return services?.AddSingleton<ISecurityService, SecurityService>()
                           // Add the claims transformer
                           .AddScoped<IClaimsTransformation, UserClaimsTransformer>()
                           // Add the system accounts
                           .AddSingleton<SystemAccounts>();
        }

        /// <summary>
        /// Initializes the data.
        /// </summary>
        /// <param name="dataService">The data service.</param>
        /// <param name="services">The services for the application.</param>
        /// <returns>The async task.</returns>
        public override Task InitializeDataAsync(IDataService? dataService, IServiceProvider? services)
        {
            if (dataService is null)
                return Task.CompletedTask;
            var UserService = new SecurityService(dataService, services?.GetService<SystemAccounts>());
            _ = UserService.LoadSystemAccount();
            _ = UserService.LoadAnonymousUserAccount();
            return Task.CompletedTask;
        }
    }
}