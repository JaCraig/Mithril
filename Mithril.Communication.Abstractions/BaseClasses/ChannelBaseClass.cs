using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Mithril.Communication.Abstractions.Interfaces;

namespace Mithril.Communication.Abstractions.BaseClasses
{
    /// <summary>
    /// Communication channel base class
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    /// <seealso cref="IChannel"/>
    public abstract class ChannelBaseClass<TChannel, TMessage> : IChannel
        where TChannel : IChannel
        where TMessage : IMessage, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelBaseClass"/> class.
        /// </summary>
        protected ChannelBaseClass(ILogger<TChannel>? logger, IFeatureManager? featureManager)
        {
            Logger = logger;
            FeatureManager = featureManager;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public abstract string Name { get; }

        /// <summary>
        /// Gets the feature manager.
        /// </summary>
        /// <value>The feature manager.</value>
        protected IFeatureManager? FeatureManager { get; }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        protected ILogger<TChannel>? Logger { get; }

        /// <summary>
        /// Determines whether this instance can handle the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>
        /// <c>true</c> if this instance can handle the specified message; otherwise, <c>false</c>.
        /// </returns>
        public bool CanHandle(IMessage? message)
        {
            return message is TMessage;
        }

        /// <summary>
        /// Creates a message object.
        /// </summary>
        /// <returns>The message related to the channel.</returns>
        public virtual IMessage CreateMessage()
        {
            return new TMessage();
        }

        /// <summary>
        /// Sends the message asynchronously.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The message result</returns>
        public abstract Task<MessageResult> SendMessageAsync(IMessage? message);
    }
}