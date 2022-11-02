using Canister.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mithril.API.Abstractions.Commands.Interfaces;
using Mithril.API.Abstractions.Services;
using Mithril.API.Commands.BackgroundTasks;
using Mithril.API.Commands.Services;
using Mithril.Core.Abstractions.Modules.BaseClasses;

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
        /// Configures the services for the module.
        /// </summary>
        /// <param name="services">The services collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>Services</returns>
        public override IServiceCollection? ConfigureServices(IServiceCollection? services, IConfiguration? configuration, IHostEnvironment? environment)
        {
            return services?.AddSingleton<ICommandService, CommandService>()
                        .AddHostedService<CommandProcessorTask>();
        }

        /// <summary>
        /// Loads the module using the bootstrapper
        /// </summary>
        /// <param name="bootstrapper">The bootstrapper.</param>
        public override void Load(IBootstrapper? bootstrapper)
        {
            bootstrapper?.RegisterAll<IEventHandler>()
                .RegisterAll<ICommandEvent>()
                .RegisterAll<ICommand>()
                .RegisterAll<ICommandHandler>();
        }
    }
}