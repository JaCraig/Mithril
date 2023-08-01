using Mithril.Admin.Abstractions.Mappings;
using Mithril.Tests.Helpers;

namespace Mithril.Admin.Abstractions.Tests.Mappings
{
    /// <summary>
    /// Save model command mapping tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;SaveModelCommandMapping&gt;" />
    public class SaveModelCommandMappingTests : TestBaseClass<SaveModelCommandMapping>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SaveModelCommandMappingTests"/> class.
        /// </summary>
        public SaveModelCommandMappingTests()
        {
            TestObject = new SaveModelCommandMapping();
        }
    }
}