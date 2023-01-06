using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mithril.API.Abstractions.Attributes;
using Mithril.API.Abstractions.Commands.Interfaces;
using Mithril.API.Abstractions.Services;
using Mithril.API.Commands.BackgroundTasks;
using Mithril.API.Commands.Services;
using Mithril.API.Commands.Utils;
using Mithril.Core.Abstractions.Extensions;
using Mithril.Core.Abstractions.Modules.BaseClasses;
using System.Reflection;

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
            if (endpoints is null || Services is null)
                return endpoints;
            MethodInfo? EndPointMethod = typeof(CommandEndpointBuilder).GetMethod(nameof(CommandEndpointBuilder.SetupEndPoint), BindingFlags.Static | BindingFlags.Public);
            var TempProvider = endpoints.ServiceProvider;
            Core.Abstractions.Configuration.MithrilConfig? SystemConfig = configuration.GetSystemConfig();
            var CommandEndpoint = SystemConfig?.API?.CommandEndpoint ?? "/api/command/";
            foreach (ICommandHandler Handler in TempProvider.GetServices<ICommandHandler>().Where(x => x.GetType().GetCustomAttribute<ApiIgnoreAttribute>() is null))
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
            services?.AddAllTransient<IEventHandler>()
                .AddAllTransient<IEvent>()
                .AddAllTransient<ICommand>()
                .AddAllTransient<ICommandHandler>();
            return Services;
        }
    }
}