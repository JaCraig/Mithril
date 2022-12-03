using Canister.Interfaces;
using GraphQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mithril.API.Abstractions.Query.Interfaces;
using Mithril.API.GraphQL.Authorization;
using Mithril.API.GraphQL.GraphTypes.Builder;
using Mithril.API.GraphQL.ObjectGraphs;
using Mithril.Core.Abstractions.Extensions;
using Mithril.Core.Abstractions.Modules.BaseClasses;

namespace Mithril.API.GraphQL
{
    /// <summary>
    /// API Module
    /// </summary>
    /// <seealso cref="ModuleBaseClass&lt;APIModule&gt;"/>
    public class GraphQLModule : ModuleBaseClass<GraphQLModule>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphQLModule"/> class.
        /// </summary>
        public GraphQLModule()
            : base("GraphQL Module", "API", "API", "GraphQL")
        {
        }

        /// <summary>
        /// Gets the order that they are initialized in.
        /// </summary>
        /// <value>The order that they are initialized in.</value>
        public override int Order => int.MaxValue;

        /// <summary>
        /// Configures the application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>Application builder</returns>
        public override IApplicationBuilder? ConfigureApplication(IApplicationBuilder? app, IConfiguration? configuration, IHostEnvironment? environment)
        {
            var Settings = configuration.GetSystemConfig();
            // GraphQL endpoint
            return app?.UseGraphQL<CompositeSchema>(configuration.GetSystemConfig()?.API?.QueryEndpoint ?? "/api/query", options =>
            {
                if (!(Settings?.API?.AllowAnonymous ?? false))
                    options.AuthorizationRequired = true;
                if (!string.IsNullOrEmpty(Settings?.API?.AuthorizationPolicy))
                    options.AuthorizedPolicy = Settings?.API?.AuthorizationPolicy;
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
            services?.AddSingleton<CompositeSchema>();
            services?.AddSingleton<GraphTypeManager>();
            services?.AddGraphQL(config =>
            {
                config.ConfigureExecutionOptions((options) =>
                {
                    options.EnableMetrics = false;
                    options.UnhandledExceptionDelegate = ctx =>
                    {
                        ctx.Context?.RequestServices?.GetService<ILogger<GraphQLModule>>()?.LogError("{Error} occured", ctx.OriginalException.Message);
                        return Task.CompletedTask;
                    };
                })
                ?.AddSystemTextJson()
                ?.AddUserContextBuilder((context) => new GraphQLUserContextDictionary(context.User))
                ?.AddAuthorizationRule();
            });
            return services;
        }

        /// <summary>
        /// Loads the module using the bootstrapper
        /// </summary>
        /// <param name="bootstrapper">The bootstrapper.</param>
        public override void Load(IBootstrapper? bootstrapper)
        {
            bootstrapper?.RegisterAll<IQuery>(ServiceLifetime.Singleton);
        }
    }
}