using Mithril.Admin.Abstractions.DataEditor;
using Mithril.Tests.Helpers;

namespace Mithril.Admin.Abstractions.Tests.DataEditor
{
    /// <summary>
    /// Property metadata tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;PropertyMetadata&gt;" />
    public class PropertyMetadataTests : TestBaseClass<PropertyMetadata>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyMetadataTests"/> class.
        /// </summary>
        public PropertyMetadataTests()
        {
            TestObject = new PropertyMetadata(null);
            ObjectType = typeof(PropertyMetadata);
        }
    }
}