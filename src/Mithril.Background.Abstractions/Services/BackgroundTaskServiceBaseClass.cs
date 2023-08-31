using Mithril.Background.Abstractions.Interfaces;

namespace Mithril.Background.Abstractions.Services
{
    /// <summary>
    /// Background task service base class
    /// </summary>
    /// <seealso cref="IBackgroundTaskService" />
    public abstract class BackgroundTaskServiceBaseClass : IBackgroundTaskService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BackgroundTaskServiceBaseClass"/> class.
        /// </summary>
        /// <param name="scheduledTasks">The scheduled tasks.</param>
        protected BackgroundTaskServiceBaseClass(IEnumerable<IScheduledTask> scheduledTasks)
        {
            ScheduledTasks = scheduledTasks;
            ScheduledTaskTimer = new Timer(CheckScheduledTasks, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
        }

        /// <summary>
        /// Gets the scheduled tasks.
        /// </summary>
        /// <value>
        /// The scheduled tasks.
        /// </value>
        protected IEnumerable<IScheduledTask> ScheduledTasks { get; }

        /// <summary>
        /// Gets the scheduled task timer.
        /// </summary>
        /// <value>
        /// The scheduled task timer.
        /// </value>
        protected Timer? ScheduledTaskTimer { get; private set; }

        /// <summary>
        /// Gets or sets the tasks.
        /// </summary>
        /// <value>
        /// The tasks.
        /// </value>
        protected Queue<IBackgroundTask> Tasks { get; } = new Queue<IBackgroundTask>();

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
        /// Enqueues the specified task.
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns>
        /// This.
        /// </returns>
        public IBackgroundTaskService Enqueue(params IBackgroundTask[] tasks)
        {
            foreach (IBackgroundTask Task in tasks)
            {
                Tasks.Enqueue(Task);
            }
            return this;
        }

        /// <summary>
        /// Executes any queued background tasks.
        /// </summary>
        /// <returns>
        /// Async task
        /// </returns>
        public abstract Task ExecuteAsync();

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
                ScheduledTaskTimer?.Dispose();
                ScheduledTaskTimer = null;
            }
        }

        /// <summary>
        /// Checks the scheduled tasks.
        /// </summary>
        /// <param name="state">The state.</param>
        private void CheckScheduledTasks(object? state)
        {
            foreach (IScheduledTask? Task in ScheduledTasks.Where(task => task.Frequencies.Any(frequency => frequency.CanRun(task.LastRunTime, DateTime.Now))))
            {
                Task.LastRunTime = DateTime.Now;
                _ = Enqueue(Task);
            }
        }
    }
}