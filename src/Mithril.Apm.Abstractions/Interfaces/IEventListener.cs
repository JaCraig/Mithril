using Mithril.Apm.Abstractions.Services;

namespace Mithril.Apm.Abstractions.Interfaces
{
    /// <summary>
    /// Event listener interface
    /// </summary>
    public interface IEventListener
    {
        /// <summary>
        /// Subscribes the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns>This.</returns>
        IEventListener Subscribe(IMetricsCollectorService observer);
    }
}