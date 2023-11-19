using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Mithril.API.Abstractions.Configuration;
using Mithril.API.Abstractions.Services;
using Mithril.Background.Abstractions.Frequencies;
using Mithril.Background.Abstractions.Interfaces;

namespace Mithril.API.Commands.BackgroundTasks
{
    /// <summary>
    /// Event processor task
    /// </summary>
    /// <seealso cref="IHostedService"/>
    /// <seealso cref="IDisposable"/>
    /// <remarks>
    /// Initializes a new instance of the <see cref="EventProcessorTask" /> class.
    /// </remarks>
    /// <param name="eventService">The event service.</param>
    /// <param name="configuration">The configuration.</param>
    public class EventProcessorTask(IEventService? eventService, IOptions<APIOptions>? configuration) : IScheduledTask
    {
        /// <summary>
        /// Gets the frequencies that the task is run at.
        /// </summary>
        /// <value>
        /// The frequencies the task is run at.
        /// </value>
        public IFrequency[] Frequencies { get; } = [new RunEvery(TimeSpan.FromSeconds(configuration?.Value?.EventRunFrequency ?? 60))];

        /// <summary>
        /// Gets the last run time.
        /// </summary>
        /// <value>
        /// The last run time.
        /// </value>
        public DateTime LastRunTime { get; set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; } = "Event Processor";

        /// <summary>
        /// Gets the Event service.
        /// </summary>
        /// <value>The Event service.</value>
        private IEventService? EventService { get; } = eventService;

        /// <summary>
        /// Executes this instance.
        /// </summary>
        /// <returns>
        /// Async task.
        /// </returns>
        public Task ExecuteAsync() => EventService?.ProcessAsync() ?? Task.CompletedTask;
    }
}