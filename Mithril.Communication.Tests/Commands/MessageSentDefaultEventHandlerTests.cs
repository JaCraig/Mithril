using Mithril.Communication.Commands;
using Mithril.Tests.Helpers;

namespace Mithril.Communication.Tests.Commands
{
    /// <summary>
    /// Message sent default event handler tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;MessageSentDefaultEventHandler&gt;"/>
    public class MessageSentDefaultEventHandlerTests : TestBaseClass<MessageSentDefaultEventHandler>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageSentDefaultEventHandlerTests"/> class.
        /// </summary>
        public MessageSentDefaultEventHandlerTests()
        {
            TestObject = new MessageSentDefaultEventHandler(null, null);
        }
    }
}