using Mithril.Tests.Helpers;

namespace Mithril.Communication.Abstractions.Tests
{
    /// <summary>
    /// Message result tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;MessageResult&gt;"/>
    public class MessageResultTests : TestBaseClass<MessageResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageResultTests"/> class.
        /// </summary>
        public MessageResultTests()
        {
            TestObject = new MessageResult("Message");
        }
    }
}