using Mithril.Admin.Abstractions.Mappings;
using Mithril.Tests.Helpers;

namespace Mithril.Admin.Abstractions.Tests.Mappings
{
    /// <summary>
    /// Model saved event mapping tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;ModelSavedEventMapping&gt;" />
    public class ModelSavedEventMappingTests : TestBaseClass<ModelSavedEventMapping>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelSavedEventMappingTests"/> class.
        /// </summary>
        public ModelSavedEventMappingTests()
        {
            TestObject = new ModelSavedEventMapping();
        }
    }
}