namespace Mithril.Background.Abstractions.Interfaces
{
    /// <summary>
    /// Background task
    /// </summary>
    public interface IBackgroundTask
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        /// <returns>Async task.</returns>
        Task ExecuteAsync();
    }
}