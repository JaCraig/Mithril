using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mithril.Core.Abstractions.Extensions;
using Mithril.Core.Abstractions.Modules.BaseClasses;
using Mithril.HealthChecks.Abstractions.Configuration;
using Mithril.HealthChecks.Abstractions.Interfaces;
using Mithril.HealthChecks.Abstractions.Services;
using Mithril.HealthChecks.HealthChecks;
using Mithril.HealthChecks.Services;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mithril.HealthChecks
{
    /// <summary>
    /// Health check module
    /// </summary>
    /// <seealso cref="ModuleBaseClass&lt;HealthCheckModule&gt;"/>
    public class HealthCheckModule : ModuleBaseClass<HealthCheckModule>
    {
        /// <summary>
        /// Gets the order that they are initialized in.
        /// </summary>
        /// <value>The order that they are initialized in.</value>
        public override int Order => int.MinValue;

        /// <summary>
        /// Configures the routes.
        /// </summary>
        /// <param name="endpoints">The endpoints.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>Endpoint route builder</returns>
        public override IEndpointRouteBuilder? ConfigureRoutes(IEndpointRouteBuilder? endpoints, IConfiguration? configuration, IHostEnvironment? environment)
        {
            if (endpoints?.ServiceProvider.GetService<IResponseFormatterService>() is null)
                return endpoints;
            Core.Abstractions.Configuration.MithrilConfig? SystemConfig = configuration?.GetSystemConfig();
            var JsonConfig = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            JsonConfig.Converters.Add(new JsonStringEnumConverter());
            var HealthCheckEndPoint = configuration.GetConfig<MithrilHealthCheckOptions>("Mithril:HealthChecks")?.CheckEndPoint ?? "/api/healthchecks";
            IEndpointConventionBuilder? EndpointBuilder = endpoints?.MapHealthChecks(HealthCheckEndPoint + ".{format}", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = (context, result) =>
                {
                    IResponseFormatterService? Formatter = context.RequestServices.GetService<IResponseFormatterService>();
                    return Formatter?.FormatResponse(context, result) ?? Task.CompletedTask;
                }
            });
            if (!string.IsNullOrEmpty(SystemConfig?.Security?.DefaultCorsPolicy))
                EndpointBuilder?.RequireCors(SystemConfig.Security.DefaultCorsPolicy);
            EndpointBuilder = endpoints?.MapHealthChecks(HealthCheckEndPoint, new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = (context, result) =>
                {
                    IResponseFormatterService? Formatter = context.RequestServices.GetService<IResponseFormatterService>();
                    return Formatter?.FormatResponse(context, result) ?? Task.CompletedTask;
                }
            });
            if (!string.IsNullOrEmpty(SystemConfig?.Security?.DefaultCorsPolicy))
                EndpointBuilder?.RequireCors(SystemConfig.Security.DefaultCorsPolicy);
            return endpoints;
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
            if (configuration is null || services is null)
                return services;

            // Set up config.
            services = services.Configure<MithrilHealthCheckOptions>(configuration.GetSection("Mithril:HealthChecks"));

            var Timeout = configuration.GetConfig<MithrilHealthCheckOptions>("Mithril:HealthChecks")?.DefaultTimeout ?? 3;
            services.AddHealthChecks()
                .AddCheck<SystemStatusHealthCheck>("System", null, new string[] { "System" }, new TimeSpan(0, 0, Timeout));
            services.AddSingleton<IResponseFormatterService, ResponseFormatterService>();
            services.AddAllTransient<IResponseFormatter>();
            return services;
        }
    }
}