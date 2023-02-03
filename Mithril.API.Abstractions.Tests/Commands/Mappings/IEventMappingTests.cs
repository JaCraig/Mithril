using Mithril.API.Abstractions.Commands.Mappings;
using Mithril.Tests.Helpers;

namespace Mithril.API.Abstractions.Tests.Commands.Mappings
{
    /// <summary>
    /// IEventMapping tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;IEventMapping&gt;"/>
    public class IEventMappingTests : TestBaseClass<IEventMapping>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IEventMappingTests"/> class.
        /// </summary>
        public IEventMappingTests()
        {
            TestObject = new IEventMapping();
        }
    }
}