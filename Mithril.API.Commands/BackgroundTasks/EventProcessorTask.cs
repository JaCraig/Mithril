using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mithril.API.Abstractions.Configuration;
using Mithril.API.Abstractions.Services;
using Mithril.Core.Abstractions.BaseClasses;

namespace Mithril.API.Commands.BackgroundTasks
{
    /// <summary>
    /// Event processor task
    /// </summary>
    /// <seealso cref="IHostedService"/>
    /// <seealso cref="IDisposable"/>
    public class EventProcessorTask : HostedServiceBaseClass<EventProcessorTask>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventProcessorTask"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="eventService">The event service.</param>
        /// <param name="configuration">The configuration.</param>
        public EventProcessorTask(ILogger<EventProcessorTask>? logger, IEventService? eventService, IOptions<APIOptions>? configuration)
            : base(logger, configuration?.Value?.EventRunFrequency ?? 60)
        {
            EventService = eventService;
        }

        /// <summary>
        /// Gets the Event service.
        /// </summary>
        /// <value>The Event service.</value>
        private IEventService? EventService { get; }

        /// <summary>
        /// Does the work.
        /// </summary>
        /// <returns>The async task.</returns>
        protected override Task DoWorkAsync()
        {
            return EventService?.ProcessAsync() ?? Task.CompletedTask;
        }
    }
}