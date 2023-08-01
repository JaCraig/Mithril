using Mithril.Admin.Queries;
using Mithril.Tests.Helpers;

namespace Mithril.Admin.Tests.Queries
{
    /// <summary>
    /// Entities query tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;EntitiesQuery&gt;" />
    public class EntitiesQueryTests : TestBaseClass<EntitiesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntitiesQueryTests"/> class.
        /// </summary>
        public EntitiesQueryTests()
        {
            TestObject = new EntitiesQuery(null, null, null);
            ObjectType = typeof(EntitiesQuery);
        }
    }
}