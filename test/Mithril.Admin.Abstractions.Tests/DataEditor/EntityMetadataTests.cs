using Mithril.Admin.Abstractions.DataEditor;
using Mithril.Tests.Helpers;

namespace Mithril.Admin.Abstractions.Tests.DataEditor
{
    /// <summary>
    /// Entity metadata tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;EntityMetadata&gt;" />
    public class EntityMetadataTests : TestBaseClass<EntityMetadata>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityMetadataTests"/> class.
        /// </summary>
        public EntityMetadataTests()
        {
            TestObject = new EntityMetadata(typeof(EntityMetadata));
            ObjectType = typeof(EntityMetadata);
        }
    }
}