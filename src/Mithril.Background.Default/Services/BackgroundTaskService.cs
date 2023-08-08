using Microsoft.Extensions.Logging;
using Mithril.Background.Abstractions.Interfaces;
using Mithril.Background.Abstractions.Services;

namespace Mithril.Background.Default.Services
{
    /// <summary>
    /// Background task service
    /// </summary>
    /// <seealso cref="BackgroundTaskServiceBaseClass" />
    public class BackgroundTaskService : BackgroundTaskServiceBaseClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BackgroundTaskService" /> class.
        /// </summary>
        /// <param name="scheduledTasks">The scheduled tasks.</param>
        /// <param name="logger">The logger.</param>
        public BackgroundTaskService(IEnumerable<IScheduledTask>? scheduledTasks, ILogger<BackgroundTaskService>? logger)
            : base(scheduledTasks ?? Array.Empty<IScheduledTask>())
        {
            Logger = logger;
        }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        private ILogger<BackgroundTaskService>? Logger { get; }

        /// <summary>
        /// Executes any queued background tasks.
        /// </summary>
        public override async Task ExecuteAsync()
        {
            var CurrentTasks = new List<Task>();
            while (Tasks.TryDequeue(out var Task))
            {
                Logger?.LogInformation("Running {TaskName}", Task.Name);
                CurrentTasks.Add(Task.ExecuteAsync());
            }
            await Task.WhenAll(CurrentTasks).ConfigureAwait(false);
        }
    }
}