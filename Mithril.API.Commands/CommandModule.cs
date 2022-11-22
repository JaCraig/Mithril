using Canister.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mithril.API.Abstractions.Attributes;
using Mithril.API.Abstractions.Commands.Interfaces;
using Mithril.API.Abstractions.Services;
using Mithril.API.Commands.BackgroundTasks;
using Mithril.API.Commands.Endpoint;
using Mithril.API.Commands.Services;
using Mithril.Core.Abstractions.Configuration;
using Mithril.Core.Abstractions.Extensions;
using Mithril.Core.Abstractions.Modules.BaseClasses;
using Mithril.Data.Abstractions.Services;
using System.Reflection;
using System.Security.Claims;

namespace Mithril.API.Commands
{
    /// <summary>
    /// Command module
    /// </summary>
    /// <seealso cref="ModuleBaseClass&lt;CommandModule&gt;"/>
    public class CommandModule : ModuleBaseClass<CommandModule>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandModule"/> class.
        /// </summary>
        public CommandModule()
            : base("Command Module", "API", "API", "CQRS", "Command")
        {
        }

        /// <summary>
        /// Gets or sets the services.
        /// </summary>
        /// <value>The services.</value>
        private IServiceCollection? Services { get; set; }

        /// <summary>
        /// Configures the routes.
        /// </summary>
        /// <param name="endpoints">The endpoints.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>Endpoint route builder</returns>
        public override IEndpointRouteBuilder? ConfigureRoutes(IEndpointRouteBuilder? endpoints, IConfiguration? configuration, IHostEnvironment? environment)
        {
            if (endpoints is null)
                return endpoints;
            var EndPointMethod = typeof(CommandModule).GetMethod(nameof(SetupEndPoint), BindingFlags.Static | BindingFlags.NonPublic);
            var TempProvider = Services.BuildServiceProvider();
            var SystemConfig = configuration.GetSystemConfig();
            var CommandEndpoint = (SystemConfig?.API?.CommandEndpoint ?? "/api/command/");
            foreach (var Handler in TempProvider.GetServices<ICommandHandler>())
            {
                EndPointMethod?.MakeGenericMethod(Handler.ViewModelType).Invoke(this, new object?[] { endpoints, CommandEndpoint, Handler, SystemConfig });
            }
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
            Services = services?.AddSingleton<ICommandService, CommandService>()
                        .AddSingleton<IEventService, EventService>()
                        .AddHostedService<CommandProcessorTask>()
                        .AddHostedService<EventProcessorTask>();
            return Services;
        }

        /// <summary>
        /// Loads the module using the bootstrapper
        /// </summary>
        /// <param name="bootstrapper">The bootstrapper.</param>
        public override void Load(IBootstrapper? bootstrapper)
        {
            bootstrapper?.RegisterAll<IEventHandler>()
                .RegisterAll<IEvent>()
                .RegisterAll<ICommand>()
                .RegisterAll<ICommandHandler>();
        }

        /// <summary>
        /// Setups the end point.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the view model.</typeparam>
        /// <param name="endpoints">The endpoints.</param>
        /// <param name="commandEndPoint">The command end point.</param>
        /// <param name="commandHandler">The command handler.</param>
        /// <param name="config">The configuration.</param>
        private static void SetupEndPoint<TViewModel>(IEndpointRouteBuilder? endpoints, string commandEndPoint, ICommandHandler<TViewModel> commandHandler, MithrilConfig? config)
            where TViewModel : notnull
        {
            var EndPointBuilder = endpoints?.MapPost(commandEndPoint + commandHandler.CommandName, (
                                                        IDataService dataService,
                                                        ILogger<CommandModule> logger,
                                                        ClaimsPrincipal user,
                                                        TViewModel value) => CommandEndpoint.RequestDelegate(dataService, logger, user, commandHandler, value))
                                            .Produces<ReturnedResult>(StatusCodes.Status200OK, contentType: "application/json")
                                            .Produces<ReturnedResult>(StatusCodes.Status400BadRequest, contentType: "application/json")
                                            .WithName(commandHandler.CommandName)
                                            .WithTags(commandHandler.Tags);
            if (EndPointBuilder is null)
                return;
            if (commandHandler.ContentTypeAccepts.Length > 1)
                EndPointBuilder = EndPointBuilder.Accepts<TViewModel>(commandHandler.ContentTypeAccepts[0], commandHandler.ContentTypeAccepts[1..^1]);
            else if (commandHandler.ContentTypeAccepts.Length > 0)
                EndPointBuilder = EndPointBuilder.Accepts<TViewModel>(commandHandler.ContentTypeAccepts[0]);

            var HandlerType = commandHandler.GetType();

            var AuthorizationAttribute = HandlerType.GetCustomAttribute<ApiAuthorizeAttribute>();

            if ((config?.API?.AllowAnonymous ?? false) || HandlerType.GetCustomAttribute<ApiAllowAnonymousAttribute>() is not null)
            {
                EndPointBuilder = EndPointBuilder?.AllowAnonymous();
            }
            else if (!string.IsNullOrEmpty(AuthorizationAttribute?.PolicyName) || !string.IsNullOrEmpty(config?.API?.AuthorizationPolicy))
            {
                EndPointBuilder = EndPointBuilder?.Produces(StatusCodes.Status401Unauthorized);
                EndPointBuilder = EndPointBuilder?.RequireAuthorization(AuthorizationAttribute?.PolicyName ?? config?.API?.AuthorizationPolicy ?? "");
            }
            else
            {
                EndPointBuilder = EndPointBuilder?.Produces(StatusCodes.Status401Unauthorized);
                EndPointBuilder = EndPointBuilder?.RequireAuthorization();
            }
        }
    }
}