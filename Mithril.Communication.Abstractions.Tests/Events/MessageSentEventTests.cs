using Mithril.Communication.Abstractions.Events;
using Mithril.Tests.Helpers;

namespace Mithril.Communication.Abstractions.Tests.Events
{
    /// <summary>
    /// Message sent event tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;MessageSentEvent&gt;"/>
    public class MessageSentEventTests : TestBaseClass<MessageSentEvent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageSentEventTests"/> class.
        /// </summary>
        public MessageSentEventTests()
        {
            TestObject = new MessageSentEvent();
        }
    }
}