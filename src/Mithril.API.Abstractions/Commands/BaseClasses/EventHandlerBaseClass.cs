using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Mithril.API.Abstractions.Commands.Enums;
using Mithril.API.Abstractions.Commands.Interfaces;
using Mithril.Core.Abstractions.Extensions;
using Mithril.Core.Abstractions.Modules.Interfaces;

namespace Mithril.API.Abstractions.Commands.BaseClasses
{
    /// <summary>
    /// Event handler base class
    /// </summary>
    /// <seealso cref="IEventHandler"/>
    public abstract class EventHandlerBaseClass<THandler, TEvent> : IEventHandler
        where THandler : EventHandlerBaseClass<THandler, TEvent>
        where TEvent : IEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerBaseClass{THandler,TEvent}" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="featureManager">The feature manager.</param>
        protected EventHandlerBaseClass(ILogger<THandler>? logger, IFeatureManager? featureManager)
        {
            Logger = logger;
            FeatureManager = featureManager;
        }

        /// <summary>
        /// Gets the features associated with this command.
        /// </summary>
        /// <value>The features associated with this command.</value>
        public virtual IFeature[] Features { get; } = Array.Empty<IFeature>();

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public virtual string Name { get; } = typeof(THandler).Name;

        /// <summary>
        /// Gets the feature manager.
        /// </summary>
        /// <value>The feature manager.</value>
        protected IFeatureManager? FeatureManager { get; }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        protected ILogger<THandler>? Logger { get; }

        /// <summary>
        /// Determines if this event handler accepts the event.
        /// </summary>
        /// <param name="arg">The argument.</param>
        /// <returns>True if it accepts it, false otherwise.</returns>
        public virtual bool Accepts(IEvent arg) => arg is TEvent;

        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="arg">The argument.</param>
        /// <returns>The result from processing the event.</returns>
        public EventResult Handle(IEvent arg)
        {
            return arg is null || !Accepts(arg)
                ? new EventResult(arg, EventStateTypes.Error, this, new ArgumentNullException(nameof(arg)))
                : Handle((TEvent)arg);
        }

        /// <summary>
        /// Handles the specified argument.
        /// </summary>
        /// <param name="arg">The argument.</param>
        /// <returns>The result from processing the event.</returns>
        protected abstract EventResult Handle(TEvent arg);

        /// <summary>
        /// Determines whether the associated features are enabled.
        /// </summary>
        /// <returns><c>true</c> if all features are enabled; otherwise, <c>false</c>.</returns>
        protected bool IsFeatureEnabled() => FeatureManager.AreFeaturesEnabled(Features);
    }
}