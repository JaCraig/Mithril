﻿using GraphQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mithril.API.Abstractions.Configuration;
using Mithril.API.Abstractions.Query.Interfaces;
using Mithril.API.Abstractions.Services;
using Mithril.API.GraphQL.Authorization;
using Mithril.API.GraphQL.GraphTypes.Builder;
using Mithril.API.GraphQL.ObjectGraphs;
using Mithril.API.GraphQL.Services;
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
            APIOptions? Settings = configuration.GetConfig<APIOptions>("Mithril:API");

            // GraphQL endpoint
            return app?.UseGraphQL<CompositeSchema>(Settings?.QueryEndpoint ?? "/api/query", options =>
            {
                if (!(Settings?.AllowAnonymous ?? false))
                    options.AuthorizationRequired = true;
                if (!string.IsNullOrEmpty(Settings?.AuthorizationPolicy))
                    options.AuthorizedPolicy = Settings?.AuthorizationPolicy;
            });
        }

        /// <summary>
        /// Configures the routes.
        /// </summary>
        /// <param name="endpoints">The endpoints.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>Endpoint route builder</returns>
        public override IEndpointRouteBuilder? ConfigureRoutes(IEndpointRouteBuilder? endpoints, IConfiguration? configuration, IHostEnvironment? environment)
        {
            Core.Abstractions.Configuration.MithrilConfig? Settings = configuration?.GetSystemConfig();
            APIOptions? APIConfig = configuration.GetConfig<APIOptions>("Mithril:API");
            GraphQLEndpointConventionBuilder? EndpointBuilder = endpoints?.MapGraphQL(APIConfig?.QueryEndpoint ?? "/api/query");
            if (!string.IsNullOrEmpty(Settings?.Security?.DefaultCorsPolicy))
                _ = (EndpointBuilder?.RequireCors(Settings.Security.DefaultCorsPolicy));
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
            _ = (services?.AddSingleton<CompositeSchema>());
            _ = (services?.AddSingleton<GraphTypeManager>());
            _ = (services?.AddGraphQL(config =>
            {
                _ = (config.ConfigureExecutionOptions((options) =>
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
                ?.AddAuthorizationRule());
            }));
            _ = (services?.AddAllSingleton<IQuery>()
                ?.AddAllSingleton<IDropDownQuery>()
                ?.AddSingleton<IQueryService, QueryService>()
                ?.AddSingleton<IDropDownQueryService, DropDownQueryService>());
            return services;
        }
    }
}