using BigBook;
using Inflatable;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Mithril.API.Abstractions.Commands.Interfaces;
using Mithril.API.Abstractions.Services;
using Mithril.Core.Abstractions.Configuration;
using Mithril.Core.Abstractions.Extensions;
using System.Diagnostics;
using System.Dynamic;

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
        /// <param name="eventHandlers">The event handlers.</param>
        /// <param name="commandHandlers">The command handlers.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="configuration">The configuration.</param>
        public CommandService(IEnumerable<IEventHandler> eventHandlers, IEnumerable<ICommandHandler> commandHandlers, ILogger<CommandService> logger, IConfiguration configuration)
        {
            EventHandlers = eventHandlers;
            CommandHandlers = commandHandlers.ToDictionary(x => x.CommandTypeAccepted.Name, StringComparer.OrdinalIgnoreCase);
            Logger = logger;
            Configuration = configuration.GetSystemConfig();
        }

        /// <summary>
        /// Gets the command handlers.
        /// </summary>
        /// <value>The command handlers.</value>
        private Dictionary<string, ICommandHandler> CommandHandlers { get; }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>The configuration.</value>
        private MithrilConfig? Configuration { get; }

        /// <summary>
        /// Gets the event handlers.
        /// </summary>
        /// <value>The event handlers.</value>
        private IEnumerable<IEventHandler> EventHandlers { get; }

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
        /// Converts the specified value to a command.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        /// <returns>The command based on the type.</returns>
        public ICommand? Convert(string type, ExpandoObject value)
        {
            if (!CommandHandlers.TryGetValue(type, out var Handler))
                return null;
            return Handler.Create(value);
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public async Task RunAsync()
        {
            int RunTime = Configuration?.API?.MaxCommandProcessTime ?? 40000;
            int Count = 0;
            var Context = new DbContext();
            Logger.LogInformation($"Processing commands for {RunTime} ms");
            Stopwatch.Restart();
            while (Stopwatch.ElapsedMilliseconds <= RunTime || RunTime == -1)
            {
                var Commands = DbContext<ICommand>.CreateQuery().Where(x => x.Active).Take(40).ToList();
                Logger.LogInformation($"Pulled {Commands.Count} commands");
                if (Commands.Count == 0)
                    break;
                Count += Commands.Count;
                foreach (var Key in CommandHandlers.Keys)
                {
                    var TempEvents = CommandHandlers[Key].HandleCommand(Commands.ToArray());
                    if (TempEvents.Length == 0)
                        continue;
                    Logger.LogInformation($"Created {TempEvents.Length} events");
                    for (int y = 0; y < TempEvents.Length; y++)
                    {
                        var Event = TempEvents[y];
                        Context.Delete(Event);
                        foreach (var Handler in EventHandlers.Where(x => x.AcceptsEvent(Event)))
                        {
                            Handler.HandleEvent(Event);
                        }
                    }
                    Logger.LogInformation($"Finished handling {TempEvents.Length} events");
                }
                Logger.LogInformation($"Finished processing {Count} commands.");
                Commands.ForEach(x => Context.Delete(x));
                await Context.ExecuteAsync().ConfigureAwait(false);
                Context = new DbContext();
            }
            Stopwatch.Restart();
            Count = 0;
            Context = new DbContext();
            while (Stopwatch.ElapsedMilliseconds <= RunTime || RunTime == -1)
            {
                var Events = DbContext<ICommandEvent>.CreateQuery().Where(x => x.Active).Take(100).ToList().Where(x => x.CanRun()).ToList();
                if (Events.Count == 0)
                    break;
                Count += Events.Count;
                for (int x = 0; x < Events.Count; x++)
                {
                    var Event = Events[x];
                    foreach (var Handler in EventHandlers.Where(Handler => Handler.AcceptsEvent(Event)))
                    {
                        Handler.HandleEvent(Event);
                    }
                    Context.Delete(Event);
                }
                Logger.LogInformation($"Finished processing {Count} events.");
                await Context.ExecuteAsync().ConfigureAwait(false);
                Context = new DbContext();
            }
            Stopwatch.Stop();
        }
    }
}