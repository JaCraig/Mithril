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
    /// Event service
    /// </summary>
    /// <seealso cref="IEventService"/>
    public class EventService : IEventService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventService"/> class.
        /// </summary>
        /// <param name="eventHandlers">The event handlers.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="configuration">The configuration.</param>
        public EventService(IEnumerable<IEventHandler> eventHandlers, ILogger<EventService> logger, IConfiguration configuration)
        {
            EventHandlers = eventHandlers;
            Logger = logger;
            Configuration = configuration.GetSystemConfig();
        }

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
        private ILogger<EventService> Logger { get; }

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
            int RunTime = Configuration?.API?.MaxEventProcessTime ?? 40000;
            int Count = 0;
            var Context = new DbContext();
            Logger.LogInformation($"Processing Events for {RunTime} ms");
            Stopwatch.Restart();
            Count = 0;
            Context = new DbContext();
            while (Stopwatch.ElapsedMilliseconds <= RunTime || RunTime == -1)
            {
                var Events = DbContext<IEvent>.CreateQuery().Where(x => x.Active).OrderBy(x => x.DateCreated).Take(100).ToList().Where(x => x.CanRun()).ToArray();
                Logger.LogInformation($"Pulled {Events.Length} events");
                Count += Events.Length;
                if (Events.Length == 0)
                    break;
                foreach (var Handler in EventHandlers)
                {
                    Handler.Handle(Events);
                }
                for (var x = 0; x < Events.Length; ++x)
                {
                    var Event = Events[x];
                    Event.Active = false;
                    Context.Save(Event);
                }
                await Context.ExecuteAsync().ConfigureAwait(false);
                Context = new DbContext();
                Logger.LogInformation($"Finished processing {Count} events.");
            }
            Logger.LogInformation($"Finished processing {Count} events.");
            Stopwatch.Stop();
        }
    }
}