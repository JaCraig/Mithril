﻿using BigBook;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mithril.API.Abstractions.Commands;
using Mithril.API.Abstractions.Commands.Enums;
using Mithril.API.Abstractions.Commands.Interfaces;
using Mithril.API.Abstractions.Configuration;
using Mithril.API.Abstractions.Services;
using Mithril.Data.Abstractions.Services;
using Mithril.Security.Abstractions.Services;
using System.Diagnostics;

namespace Mithril.API.Commands.Services
{
    /// <summary>
    /// Event service
    /// </summary>
    /// <seealso cref="IEventService"/>
    /// <remarks>
    /// Initializes a new instance of the <see cref="EventService"/> class.
    /// </remarks>
    /// <param name="eventHandlers">The event handlers.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="configuration">The configuration.</param>
    /// <param name="dataService">The data service.</param>
    /// <param name="securityService">The security service.</param>
    public class EventService(
        IEnumerable<IEventHandler> eventHandlers,
        ILogger<EventService>? logger,
        IOptions<APIOptions>? configuration,
        IDataService? dataService,
        ISecurityService? securityService) : IEventService
    {
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
        /// Gets the event handlers.
        /// </summary>
        /// <value>The event handlers.</value>
        private IEnumerable<IEventHandler> EventHandlers { get; } = eventHandlers ?? Array.Empty<IEventHandler>();

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        private ILogger<EventService>? Logger { get; } = logger;

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
            if (!EventHandlers.Any())
                return;
            var RunTime = Configuration?.MaxEventProcessTime ?? 40000;
            var BatchSize = Configuration?.EventBatchSize ?? 40;
            var Count = 0;
            Logger?.LogInformation("Processing Events for {RunTime} ms", RunTime);
            Stopwatch.Restart();
            Count = 0;
            while (Stopwatch.ElapsedMilliseconds <= RunTime || RunTime == -1)
            {
                IEvent[] Events = GetEvents(BatchSize);
                Logger?.LogInformation("Pulled {EventsLength} events", Events.Length);
                Count += Events.Length;
                if (Events.Length == 0)
                    break;
                var Results = new List<EventResult>();
                foreach (IEvent Event in Events)
                {
                    Results.AddRange(EventHandlers.Where(x => x.Accepts(Event)).ForEachParallel(x => x.Handle(Event)));
                    SetEventState(Results, Event);
                    LogEventExceptions(Results, Event);
                }
                if (DataService is not null)
                {
                    _ = Events.ForEach(x => x.SetupObject(DataService, SecurityService?.LoadSystemAccount()));
                    _ = await DataService.SaveAsync(null, Events).ConfigureAwait(false);
                }
                Logger?.LogInformation("Processed {Count} events.", Count);
            }
            Logger?.LogInformation("Finished processing {Count} events.", Count);
            Stopwatch.Stop();
        }

        /// <summary>
        /// Sets the state of the event.
        /// </summary>
        /// <param name="Results">The results.</param>
        /// <param name="Event">The event.</param>
        private static void SetEventState(List<EventResult> Results, IEvent? Event)
        {
            if (Event is null)
                return;
            if (Results.Count == 0)
            {
                Event.State = EventStateTypes.Completed;
                return;
            }
            if (Results.Any(Result => Result.IsErrorState) || Results.Select(x => x.NewState).Distinct().Count() > 1)
            {
                if (Event.RetryCount <= 0)
                {
                    Event.State = EventStateTypes.Error;
                    Event.RetryCount = 0;
                }
                else
                {
                    Event.State = EventStateTypes.Retrying;
                    --Event.RetryCount;
                }
            }
            else
            {
                Event.State = Results.FirstOrDefault()?.NewState;
            }
        }

        /// <summary>
        /// Gets the events.
        /// </summary>
        /// <param name="batchSize">Size of the batch.</param>
        /// <returns></returns>
        private IEvent[] GetEvents(int batchSize) => DataService?
            .Query<IEvent>()?
            .Where(x => x.Active && x.State != "Completed" && x.State != "Error")
            .OrderBy(x => x.DateCreated)
            .Take(batchSize)
            .ToList()
            .Where(x => x.CanRun())
            .ToArray() ?? [];

        /// <summary>
        /// Logs the event exceptions.
        /// </summary>
        /// <param name="Results">The results.</param>
        /// <param name="Event">The event.</param>
        private void LogEventExceptions(List<EventResult> Results, IEvent? Event)
        {
            if (Event is null || Results.Count == 0)
                return;
            foreach (EventResult? Result in Results.Where(Result => Result.Exception is not null))
            {
                Logger?.LogError(Result.Exception, "Error when processing event {EventID} of type {EventName} by {EventHandlerName}.", Event.ID, Event.Name, Result.EventHandler.Name);
            }
        }
    }
}