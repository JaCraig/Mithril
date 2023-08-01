using Mithril.API.Abstractions.Query.Interfaces;
using Mithril.API.GraphQL.Services;
using Mithril.Tests.Helpers;

namespace Mithril.API.GraphQL.Tests.Services
{
    /// <summary>
    /// Drop down query service tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;DropDownQueryService&gt;" />
    public class DropDownQueryServiceTests : TestBaseClass<DropDownQueryService>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DropDownQueryServiceTests"/> class.
        /// </summary>
        public DropDownQueryServiceTests()
        {
            TestObject = new DropDownQueryService(Array.Empty<IDropDownQuery>());
            ObjectType = typeof(DropDownQueryService);
        }
    }
}