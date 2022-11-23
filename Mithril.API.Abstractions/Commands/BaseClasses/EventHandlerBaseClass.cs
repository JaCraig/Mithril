using Mithril.API.Abstractions.Commands.Interfaces;

namespace Mithril.API.Abstractions.Commands.BaseClasses
{
    /// <summary>
    /// Event handler base class
    /// </summary>
    /// <seealso cref="IEventHandler"/>
    public abstract class EventHandlerBaseClass<THandler> : IEventHandler
        where THandler : EventHandlerBaseClass<THandler>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerBaseClass{THandler}"/> class.
        /// </summary>
        protected EventHandlerBaseClass()
        { }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public virtual string Name { get; } = typeof(THandler).Name;

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
        /// <returns>The result from processing the event.</returns>
        public abstract EventResult Handle(IEvent arg);
    }
}