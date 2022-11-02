namespace Mithril.API.Abstractions.Commands.Interfaces
{
    /// <summary>
    /// Event handler interface
    /// </summary>
    public interface IEventHandler
    {
        /// <summary>
        /// Determines if this event handler accepts the event.
        /// </summary>
        /// <param name="arg">The argument.</param>
        /// <returns>True if it accepts it, false otherwise.</returns>
        bool AcceptsEvent(ICommandEvent arg);

        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="arg">The argument.</param>
        void HandleEvent(ICommandEvent arg);
    }
}