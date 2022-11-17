using Canister.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mithril.API.Abstractions.Commands.Interfaces;
using Mithril.API.Abstractions.Services;
using Mithril.API.Commands.BackgroundTasks;
using Mithril.API.Commands.Endpoint;
using Mithril.API.Commands.Services;
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
            var CommandEndpoint = (configuration.GetSystemConfig()?.API?.CommandEndpoint ?? "/api/command/");
            foreach (var Handler in TempProvider.GetServices<ICommandHandler>())
            {
                EndPointMethod?.MakeGenericMethod(Handler.ViewModelType).Invoke(this, new object[] { endpoints, CommandEndpoint, Handler });
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
        private static void SetupEndPoint<TViewModel>(IEndpointRouteBuilder? endpoints, string commandEndPoint, ICommandHandler<TViewModel> commandHandler)
        {
            endpoints?.MapPost(commandEndPoint + commandHandler.CommandName, (
                        IDataService dataService,
                        ClaimsPrincipal user,
                        [FromBody] TViewModel value) => CommandEndpoint.RequestDelegate(dataService, user, commandHandler, value))
                    .Produces(StatusCodes.Status200OK)
                    .Produces(StatusCodes.Status400BadRequest);
        }
    }
}