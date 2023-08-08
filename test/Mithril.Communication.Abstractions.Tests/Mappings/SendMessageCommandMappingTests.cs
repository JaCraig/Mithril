using Mithril.Communication.Abstractions.Mappings;
using Mithril.Tests.Helpers;

namespace Mithril.Communication.Abstractions.Tests.Mappings
{
    /// <summary>
    /// SendMessageCommandMapping tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;SendMessageCommandMapping&gt;"/>
    public class SendMessageCommandMappingTests : TestBaseClass<SendMessageCommandMapping>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SendMessageCommandMappingTests"/> class.
        /// </summary>
        public SendMessageCommandMappingTests()
        {
            TestObject = new SendMessageCommandMapping();
        }
    }
}