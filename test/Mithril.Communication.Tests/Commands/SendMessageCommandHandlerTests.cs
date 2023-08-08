using Mithril.Communication.Abstractions.Interfaces;
using Mithril.Communication.Commands;
using Mithril.Tests.Helpers;

namespace Mithril.Communication.Tests.Commands
{
    /// <summary>
    /// Send message command handler tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;SendMessageCommandHandler&gt;"/>
    public class SendMessageCommandHandlerTests : TestBaseClass<SendMessageCommandHandler>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SendMessageCommandHandlerTests"/> class.
        /// </summary>
        public SendMessageCommandHandlerTests()
        {
            TestObject = new SendMessageCommandHandler(null, null, null, null, null, Array.Empty<IChannel>());
            ObjectType = typeof(SendMessageCommandHandler);
        }
    }
}