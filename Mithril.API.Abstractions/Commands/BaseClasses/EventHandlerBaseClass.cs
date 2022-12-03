using BigBook;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Mithril.API.Abstractions.Commands.Interfaces;
using Mithril.Core.Abstractions.Modules.Interfaces;

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
        protected EventHandlerBaseClass(ILogger? logger, IFeatureManager? featureManager)
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
        protected ILogger? Logger { get; }

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

        /// <summary>
        /// Determines whether the associated features are enabled.
        /// </summary>
        /// <returns><c>true</c> if all features are enabled; otherwise, <c>false</c>.</returns>
        protected bool IsFeatureEnabled()
        {
            return FeatureManager is null
                || Features is null
                || Features.Length == 0
                || Features.All(x => AsyncHelper.RunSync(() => FeatureManager.IsEnabledAsync(x.Name)));
        }
    }
}