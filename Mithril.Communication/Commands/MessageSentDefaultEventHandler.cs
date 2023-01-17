using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Mithril.API.Abstractions.Commands;
using Mithril.API.Abstractions.Commands.BaseClasses;
using Mithril.API.Abstractions.Commands.Enums;
using Mithril.Communication.Abstractions.Events;

namespace Mithril.Communication.Commands
{
    /// <summary>
    /// Message sent default event handler (does nothing)
    /// </summary>
    /// <seealso cref="EventHandlerBaseClass&lt;MessageSentDefaultEventHandler, MessageSentEvent&gt;"/>
    public class MessageSentDefaultEventHandler : EventHandlerBaseClass<MessageSentDefaultEventHandler, MessageSentEvent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageSentDefaultEventHandler"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="featureManager"></param>
        public MessageSentDefaultEventHandler(ILogger<MessageSentDefaultEventHandler>? logger, IFeatureManager? featureManager) : base(logger, featureManager)
        {
        }

        /// <summary>
        /// Handles the specified argument.
        /// </summary>
        /// <param name="arg">The argument.</param>
        /// <returns>The result from processing the event.</returns>
        protected override EventResult Handle(MessageSentEvent arg)
        {
            return new EventResult(arg, EventStateTypes.Completed, this);
        }
    }
}