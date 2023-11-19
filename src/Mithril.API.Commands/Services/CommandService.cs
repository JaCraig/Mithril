using BigBook;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mithril.API.Abstractions.Commands.Interfaces;
using Mithril.API.Abstractions.Configuration;
using Mithril.API.Abstractions.Services;
using Mithril.Data.Abstractions.Services;
using Mithril.Security.Abstractions.Services;
using System.Diagnostics;

namespace Mithril.API.Commands.Services
{
    /// <summary>
    /// Command service
    /// </summary>
    /// <seealso cref="ICommandService"/>
    /// <remarks>
    /// Initializes a new instance of the <see cref="CommandService"/> class.
    /// </remarks>
    /// <param name="commandHandlers">The command handlers.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="configuration">The configuration.</param>
    /// <param name="dataService">The data service.</param>
    /// <param name="securityService">The security service.</param>
    public class CommandService(
        IEnumerable<ICommandHandler> commandHandlers,
        ILogger<CommandService>? logger,
        IOptions<APIOptions>? configuration,
        IDataService? dataService,
        ISecurityService? securityService) : ICommandService
    {
        /// <summary>
        /// Gets the command handlers.
        /// </summary>
        /// <value>The command handlers.</value>
        private IEnumerable<ICommandHandler> CommandHandlers { get; } = commandHandlers ?? Array.Empty<ICommandHandler>();

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>The configuration.</value>
        private APIOptions? Configuration { get; } = configuration?.Value;

        /// <summary>
        /// Gets the data service.
        /// </summary>
        /// <value>The data service.</value>
        private IDataService? DataService { get; } = dataService;

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        private ILogger<CommandService>? Logger { get; } = logger;

        /// <summary>
        /// Gets the security service.
        /// </summary>
        /// <value>The security service.</value>
        private ISecurityService? SecurityService { get; } = securityService;

        /// <summary>
        /// Gets the stopwatch.
        /// </summary>
        /// <value>The stopwatch.</value>
        private Stopwatch Stopwatch { get; } = new Stopwatch();

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public async Task ProcessAsync()
        {
            if (!CommandHandlers.Any())
                return;
            var RunTime = Configuration?.MaxCommandProcessTime ?? 40000;
            var BatchSize = Configuration?.CommandBatchSize ?? 40;
            var Count = 0;
            Logger?.LogInformation("Processing commands for {RunTime} ms", RunTime);
            Stopwatch.Restart();
            while (Stopwatch.ElapsedMilliseconds <= RunTime || RunTime == -1)
            {
                ICommand[] Commands = GetCommands(BatchSize);
                Logger?.LogInformation("Pulled {CommandsLength} commands", Commands.Length);
                if (Commands.Length == 0)
                    break;
                Count += Commands.Length;
                for (var x = 0; x < Commands.Length; ++x)
                {
                    ICommand Command = Commands[x];
                    var Handled = await HandleCommand(Command).ConfigureAwait(false);
                    Command.Active = !Handled;
                    Command.SetupObject(DataService, SecurityService?.LoadSystemAccount());
                }
                if (DataService is not null)
                    _ = await DataService.SaveAsync(null, Commands).ConfigureAwait(false);
                Logger?.LogInformation("Processed {Count} commands.", Count);
            }
            Logger?.LogInformation("Finished processing {Count} commands.", Count);
            Stopwatch.Stop();
        }

        /// <summary>
        /// Gets the next set of commands.
        /// </summary>
        /// <returns></returns>
        private ICommand[] GetCommands(int size) => DataService?.Query<ICommand>()?.Where(x => x.Active).OrderBy(x => x.DateCreated).Take(size).ToList().ToArray() ?? [];

        /// <summary>
        /// Handles the command.
        /// </summary>
        /// <param name="Command">The command.</param>
        private async Task<bool> HandleCommand(ICommand Command)
        {
            ICommandHandler? CommandHandler = CommandHandlers.FirstOrDefault(x => x.CanHandle(Command));
            if (CommandHandler is null)
                return true;
            try
            {
                IEvent[]? Events = await CommandHandler.HandleCommandAsync(Command).ConfigureAwait(false);
                if (Events is null || Events.Length == 0 || DataService is null)
                    return true;
                _ = await DataService.SaveAsync(null, Events).ConfigureAwait(false);
                return true;
            }
            catch (Exception e)
            {
                Logger?.LogError(e, "Error when processing command {CommandID} of type {CommandName} by {CommandHandlerName}.", Command.ID, Command.GetType().GetName(), CommandHandler?.CommandName ?? "Handler not found");
                return false;
            }
        }
    }
}