using Mithril.Communication.Abstractions.Mappings;
using Mithril.Tests.Helpers;

namespace Mithril.Communication.Abstractions.Tests.Mappings
{
    /// <summary>
    /// MessageSentEventMapping tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;MessageSentEventMapping&gt;"/>
    public class MessageSentEventMappingTests : TestBaseClass<MessageSentEventMapping>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageSentEventMappingTests"/> class.
        /// </summary>
        public MessageSentEventMappingTests()
        {
            TestObject = new MessageSentEventMapping();
        }
    }
}