namespace Mithril.API.Abstractions.Commands.Interfaces
{
    /// <summary>
    /// Event handler interface
    /// </summary>
    public interface IEventHandler
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        /// Determines if this event handler accepts the event.
        /// </summary>
        /// <param name="arg">The argument.</param>
        /// <returns>True if it accepts it, false otherwise.</returns>
        bool Accepts(IEvent arg);

        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="arg">The argument.</param>
        /// <returns>The result from processing the event.</returns>
        EventResult Handle(IEvent arg);
    }
}