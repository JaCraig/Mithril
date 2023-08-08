using Mithril.Communication.Models;
using Mithril.Tests.Helpers;

namespace Mithril.Communication.Tests.Models
{
    /// <summary>
    /// Message template tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;MessageTemplate&gt;"/>
    public class MessageTemplateTests : TestBaseClass<MessageTemplate>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageTemplateTests"/> class.
        /// </summary>
        public MessageTemplateTests()
        {
            TestObject = new MessageTemplate();
        }
    }
}