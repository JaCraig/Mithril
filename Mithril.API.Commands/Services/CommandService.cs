﻿using BigBook;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mithril.API.Abstractions.Commands.Interfaces;
using Mithril.API.Abstractions.Services;
using Mithril.Core.Abstractions.Configuration;
using Mithril.Data.Abstractions.Services;
using System.Diagnostics;

namespace Mithril.API.Commands.Services
{
    /// <summary>
    /// Command service
    /// </summary>
    /// <seealso cref="ICommandService"/>
    public class CommandService : ICommandService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandService"/> class.
        /// </summary>
        /// <param name="commandHandlers">The command handlers.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="dataService">The data service.</param>
        public CommandService(IEnumerable<ICommandHandler> commandHandlers, ILogger<CommandService>? logger, IOptions<MithrilConfig>? configuration, IDataService? dataService)
        {
            CommandHandlers = commandHandlers ?? Array.Empty<ICommandHandler>();
            Logger = logger;
            DataService = dataService;
            Configuration = configuration?.Value;
        }

        /// <summary>
        /// Gets the command handlers.
        /// </summary>
        /// <value>The command handlers.</value>
        private IEnumerable<ICommandHandler> CommandHandlers { get; }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>The configuration.</value>
        private MithrilConfig? Configuration { get; }

        /// <summary>
        /// Gets the data service.
        /// </summary>
        /// <value>The data service.</value>
        private IDataService? DataService { get; }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        private ILogger<CommandService>? Logger { get; }

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
            int RunTime = Configuration?.API?.MaxCommandProcessTime ?? 40000;
            int BatchSize = Configuration?.API?.CommandBatchSize ?? 40;
            int Count = 0;
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
                    var Command = Commands[x];
                    await HandleCommand(Command).ConfigureAwait(false);
                    Command.Active = false;
                }
                if (DataService is not null)
                    await DataService.SaveAsync(Commands).ConfigureAwait(false);
                Logger?.LogInformation("Processed {Count} commands.", Count);
            }
            Logger?.LogInformation("Finished processing {Count} commands.", Count);
            Stopwatch.Stop();
        }

        /// <summary>
        /// Gets the next set of commands.
        /// </summary>
        /// <returns></returns>
        private ICommand[] GetCommands(int size) => DataService?.Query<ICommand>()?.Where(x => x.Active).OrderBy(x => x.DateCreated).Take(size).ToList().ToArray() ?? Array.Empty<ICommand>();

        /// <summary>
        /// Handles the command.
        /// </summary>
        /// <param name="Command">The command.</param>
        private async Task HandleCommand(ICommand Command)
        {
            var Events = CommandHandlers.FirstOrDefault(x => x.CanHandle(Command))?.HandleCommand(Command);
            if (Events is null || Events.Length == 0 || DataService is null)
                return;
            await DataService.SaveAsync(Events).ConfigureAwait(false);
        }
    }
}