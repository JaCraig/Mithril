namespace Mithril.Background.Abstractions.Interfaces
{
    /// <summary>
    /// Scheduled background task
    /// </summary>
    /// <seealso cref="IBackgroundTask" />
    public interface IScheduledTask : IBackgroundTask
    {
        /// <summary>
        /// Gets the frequencies that the task is run at.
        /// </summary>
        /// <value>
        /// The frequencies the task is run at.
        /// </value>
        IFrequency[] Frequencies { get; }

        /// <summary>
        /// Gets the last run time.
        /// </summary>
        /// <value>
        /// The last run time.
        /// </value>
        DateTime LastRunTime { get; set; }
    }
}