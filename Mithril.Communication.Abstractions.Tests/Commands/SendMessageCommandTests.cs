using Mithril.Communication.Abstractions.Commands;
using Mithril.Tests.Helpers;

namespace Mithril.Communication.Abstractions.Tests.Commands
{
    /// <summary>
    /// Send Message Command tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;SendMessageCommand&gt;"/>
    public class SendMessageCommandTests : TestBaseClass<SendMessageCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SendMessageCommandTests"/> class.
        /// </summary>
        public SendMessageCommandTests()
        {
            TestObject = new SendMessageCommand();
        }
    }
}