using Mithril.Admin.Abstractions.Interfaces;
using Mithril.Admin.Services;
using Mithril.Tests.Helpers;

namespace Mithril.Admin.Tests.Services
{
    /// <summary>
    /// Entity metadata service tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;EntityMetadataService&gt;" />
    public class EntityMetadataServiceTests : TestBaseClass<EntityMetadataService>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityMetadataServiceTests"/> class.
        /// </summary>
        public EntityMetadataServiceTests()
        {
            TestObject = new EntityMetadataService(Array.Empty<IMetadataBuilder>());
            ObjectType = typeof(EntityMetadataService);
        }
    }
}