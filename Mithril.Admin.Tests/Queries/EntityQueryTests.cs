using Mithril.Admin.Queries;
using Mithril.Tests.Helpers;

namespace Mithril.Admin.Tests.Queries
{
    /// <summary>
    /// Entity query tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;EntityQuery&gt;" />
    public class EntityQueryTests : TestBaseClass<EntityQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityQueryTests"/> class.
        /// </summary>
        public EntityQueryTests()
        {
            TestObject = new EntityQuery(null, null, null);
            ObjectType = typeof(EntityQuery);
        }
    }
}