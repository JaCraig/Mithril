namespace Mithril.API.Abstractions.Services
{
    /// <summary>
    /// Event service interface
    /// </summary>
    public interface IEventService
    {
        /// <summary>
        /// Processes any new events.
        /// </summary>
        /// <returns>The async task.</returns>
        Task ProcessAsync();
    }
}