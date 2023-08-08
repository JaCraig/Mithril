using Mithril.Background.Abstractions.Interfaces;

namespace Mithril.Background.Abstractions.Services
{
    /// <summary>
    /// Background processing service
    /// </summary>
    public interface IBackgroundTaskService : IDisposable
    {
        /// <summary>
        /// Enqueues the specified task.
        /// </summary>
        /// <param name="tasks">The tasks.</param>
        /// <returns>
        /// This.
        /// </returns>
        IBackgroundTaskService Enqueue(params IBackgroundTask[] tasks);

        /// <summary>
        /// Executes any queued background tasks.
        /// </summary>
        /// <returns>Async task</returns>
        Task ExecuteAsync();
    }
}