using Mithril.API.Abstractions.Query.Interfaces;
using Mithril.API.GraphQL.Services;
using Mithril.Tests.Helpers;

namespace Mithril.API.GraphQL.Tests.Services
{
    /// <summary>
    /// Query service tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;QueryService&gt;" />
    public class QueryServiceTests : TestBaseClass<QueryService>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryServiceTests"/> class.
        /// </summary>
        public QueryServiceTests()
        {
            TestObject = new QueryService(Array.Empty<IQuery>());
            ObjectType = typeof(QueryService);
        }
    }
}