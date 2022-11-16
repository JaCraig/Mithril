using BigBook;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mithril.Core.Abstractions.Extensions;
using Mithril.Core.Abstractions.Modules.BaseClasses;
using System.Reflection;

namespace Mithril.API.Swagger
{
    /// <summary>
    /// Swagger module
    /// </summary>
    /// <seealso cref="ModuleBaseClass&lt;SwaggerModule&gt;"/>
    public class SwaggerModule : ModuleBaseClass<SwaggerModule>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SwaggerModule"/> class.
        /// </summary>
        public SwaggerModule()
            : base("Swagger Module", "API", "API", "Swagger")
        {
        }

        /// <summary>
        /// Configures the application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>Application builder</returns>
        public override IApplicationBuilder? ConfigureApplication(IApplicationBuilder? app, IConfiguration? configuration, IHostEnvironment? environment)
        {
            var SystemConfig = configuration.GetSystemConfig();
            return app?.When(environment.IsDevelopment(), app =>
            {
                app?.UseSwagger()
                       .UseSwaggerUI(conf => conf.SwaggerEndpoint(SystemConfig?.API?.OpenAPIEndpoint ?? "/swagger/v1/swagger.json",
                                                                  SystemConfig?.ApplicationName ?? Assembly.GetEntryAssembly()?.GetName().Name ?? "Mithril API V1"));
            });
        }

        /// <summary>
        /// Configures the services for the module.
        /// </summary>
        /// <param name="services">The services collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>Services</returns>
        public override IServiceCollection? ConfigureServices(IServiceCollection? services, IConfiguration? configuration, IHostEnvironment? environment)
        {
            var SystemConfig = configuration.GetSystemConfig();
            services?.AddEndpointsApiExplorer();
            services?.AddSwaggerGen();
            return services;
        }
    }
}