using Microsoft.FeatureManagement;
using Mithril.Communication.Abstractions;
using Mithril.Communication.Abstractions.BaseClasses;
using Mithril.Communication.Abstractions.Interfaces;
using Mithril.Communication.Email.Models;

namespace Mithril.Communication.Email.Channel
{
    /// <summary>
    /// Email channel for communication
    /// </summary>
    /// <seealso cref="ChannelBaseClass&lt;EmailMessage&gt;"/>
    public class EmailChannel : ChannelBaseClass<EmailMessage>
    {
        public EmailChannel(IFeatureManager? featureManager)
        {
            FeatureManager = featureManager;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public override string Name { get; } = "Email";

        /// <summary>
        /// Gets the feature manager.
        /// </summary>
        /// <value>The feature manager.</value>
        private IFeatureManager? FeatureManager { get; }

        /// <summary>
        /// Sends the message asynchronously.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The message result</returns>
        public override Task<MessageResult> SendMessageAsync(IMessage? message)
        {
            if (!CanHandle(message))
                return Task.FromResult(new MessageResult("Message is empty", new ArgumentNullException(nameof(message))));
            return Task.FromResult(new MessageResult("Sent"));
        }
    }
}