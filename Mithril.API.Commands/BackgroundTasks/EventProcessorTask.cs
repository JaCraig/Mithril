using BigBook;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mithril.API.Abstractions.Services;
using Mithril.Core.Abstractions.Configuration;

namespace Mithril.API.Commands.BackgroundTasks
{
    /// <summary>
    /// Event processor task
    /// </summary>
    /// <seealso cref="IHostedService"/>
    /// <seealso cref="IDisposable"/>
    public class EventProcessorTask : IHostedService, IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventProcessorTask"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="eventService">The event service.</param>
        /// <param name="configuration">The configuration.</param>
        public EventProcessorTask(ILogger<EventProcessorTask>? logger, IEventService? eventService, IOptions<MithrilConfig>? configuration)
        {
            Logger = logger;
            EventService = eventService;
            EventRunFrequency = configuration?.Value?.API?.EventRunFrequency ?? 60;
        }

        /// <summary>
        /// Gets the Event run frequency.
        /// </summary>
        /// <value>The Event run frequency.</value>
        private int EventRunFrequency { get; }

        /// <summary>
        /// Gets the Event service.
        /// </summary>
        /// <value>The Event service.</value>
        private IEventService? EventService { get; }

        /// <summary>
        /// Gets or sets the internal timer.
        /// </summary>
        /// <value>The internal timer.</value>
        private Timer? InternalTimer { get; set; }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        private ILogger<EventProcessorTask>? Logger { get; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Triggered when the application host is ready to start the service.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the start process has been aborted.</param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            if (EventRunFrequency == 0)
                return Task.CompletedTask;
            Logger?.LogInformation("Starting event background service");
            InternalTimer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(EventRunFrequency));
            return Task.CompletedTask;
        }

        /// <summary>
        /// Triggered when the application host is performing a graceful shutdown.
        /// </summary>
        /// <param name="cancellationToken">
        /// Indicates that the shutdown process should no longer be graceful.
        /// </param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            if (EventRunFrequency == 0)
                return Task.CompletedTask;
            Logger?.LogInformation("Stopping event background service");
            InternalTimer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="managed">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release
        /// only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool managed)
        {
            if (managed)
            {
                InternalTimer?.Dispose();
                InternalTimer = null;
            }
        }

        /// <summary>
        /// Does the work.
        /// </summary>
        /// <param name="state">The state.</param>
        private void DoWork(object? state)
        {
            AsyncHelper.RunSync(() => EventService?.ProcessAsync() ?? Task.CompletedTask);
        }
    }
}