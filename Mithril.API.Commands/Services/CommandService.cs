using BigBook;
using Inflatable;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Mithril.API.Abstractions.Commands.Interfaces;
using Mithril.API.Abstractions.Services;
using Mithril.Core.Abstractions.Configuration;
using Mithril.Core.Abstractions.Extensions;
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
        public CommandService(IEnumerable<ICommandHandler> commandHandlers, ILogger<CommandService> logger, IConfiguration configuration)
        {
            CommandHandlers = commandHandlers ?? Array.Empty<ICommandHandler>();
            Logger = logger;
            Configuration = configuration.GetSystemConfig();
        }

        /// <summary>
        /// Gets the command handlers.
        /// </summary>
        /// <value>The command handlers.</value>
        public IEnumerable<ICommandHandler> CommandHandlers { get; }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>The configuration.</value>
        private MithrilConfig? Configuration { get; }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        private ILogger<CommandService> Logger { get; }

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
            int Count = 0;
            var Context = new DbContext();
            Logger.LogInformation($"Processing commands for {RunTime} ms");
            Stopwatch.Restart();
            while (Stopwatch.ElapsedMilliseconds <= RunTime || RunTime == -1)
            {
                var Commands = DbContext<ICommand>.CreateQuery().Where(x => x.Active).OrderBy(x => x.DateCreated).Take(40).ToList().ToArray();
                Logger.LogInformation($"Pulled {Commands.Length} commands");
                if (Commands.Length == 0)
                    break;
                Count += Commands.Length;
                foreach (var Handler in CommandHandlers)
                {
                    _ = Handler.HandleCommand(Commands);
                }
                for (var x = 0; x < Commands.Length; ++x)
                {
                    var Command = Commands[x];
                    Command.Active = false;
                    Context.Save(Command);
                }
                await Context.ExecuteAsync().ConfigureAwait(false);
                Context = new DbContext();
                Logger.LogInformation($"Finished processing {Count} commands.");
            }
            Logger.LogInformation($"Finished processing {Count} commands.");
            Stopwatch.Stop();
        }
    }
}