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
    /// <remarks>
    /// Initializes a new instance of the <see cref="MessageSentDefaultEventHandler"/> class.
    /// </remarks>
    /// <param name="logger"></param>
    /// <param name="featureManager"></param>
    public class MessageSentDefaultEventHandler(ILogger<MessageSentDefaultEventHandler>? logger, IFeatureManager? featureManager) : EventHandlerBaseClass<MessageSentDefaultEventHandler, MessageSentEvent>(logger, featureManager)
    {
        /// <summary>
        /// Handles the specified argument.
        /// </summary>
        /// <param name="arg">The argument.</param>
        /// <returns>The result from processing the event.</returns>
        protected override EventResult Handle(MessageSentEvent arg) => new(arg, EventStateTypes.Completed, this);
    }
}