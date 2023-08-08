using Mithril.Communication.Abstractions.Mappings;
using Mithril.Tests.Helpers;

namespace Mithril.Communication.Abstractions.Tests.Mappings
{
    /// <summary>
    /// IMessageMapping tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;IMessageMapping&gt;"/>
    public class IMessageMappingTests : TestBaseClass<IMessageMapping>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IMessageMappingTests"/> class.
        /// </summary>
        public IMessageMappingTests()
        {
            TestObject = new IMessageMapping();
        }
    }
}