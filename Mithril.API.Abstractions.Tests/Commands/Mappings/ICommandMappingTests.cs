using Mithril.API.Abstractions.Commands.Mappings;
using Mithril.Tests.Helpers;

namespace Mithril.API.Abstractions.Tests.Commands.Mappings
{
    /// <summary>
    /// ICommandMapping tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;ICommandMapping&gt;"/>
    public class ICommandMappingTests : TestBaseClass<ICommandMapping>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ICommandMappingTests"/> class.
        /// </summary>
        public ICommandMappingTests()
        {
            TestObject = new ICommandMapping();
        }
    }
}