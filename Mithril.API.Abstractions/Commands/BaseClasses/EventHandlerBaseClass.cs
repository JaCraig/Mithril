using Mithril.API.Abstractions.Commands.Interfaces;

namespace Mithril.API.Abstractions.Commands.BaseClasses
{
    public abstract class EventHandlerBaseClass : IEventHandler
    {
        /// <summary>
        /// Determines if this event handler accepts the event.
        /// </summary>
        /// <param name="arg">The argument.</param>
        /// <returns>True if it accepts it, false otherwise.</returns>
        public abstract bool Accepts(IEvent arg);

        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="arg">The argument.</param>
        public void Handle(params IEvent[] arg)
        {
            HandleEvents(arg?.Where(x => Accepts(x)).ToArray() ?? Array.Empty<IEvent>());
        }

        /// <summary>
        /// Handles the events.
        /// </summary>
        /// <param name="arg">The argument.</param>
        protected abstract void HandleEvents(IEvent[] arg);
    }
}