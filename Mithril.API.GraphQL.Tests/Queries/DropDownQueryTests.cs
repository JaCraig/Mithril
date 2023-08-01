using Mithril.API.GraphQL.Queries;
using Mithril.Tests.Helpers;

namespace Mithril.API.GraphQL.Tests.Queries
{
    /// <summary>
    /// Drop down query tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;DropDownQuery&gt;" />
    public class DropDownQueryTests : TestBaseClass<DropDownQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DropDownQueryTests"/> class.
        /// </summary>
        public DropDownQueryTests()
        {
            TestObject = new DropDownQuery(null, null, null);
        }
    }
}