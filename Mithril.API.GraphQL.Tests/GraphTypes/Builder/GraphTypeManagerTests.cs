using Mithril.API.GraphQL.GraphTypes.Builder;
using Mithril.Tests.Helpers;

namespace Mithril.API.GraphQL.Tests.GraphTypes.Builder
{
    public class GraphTypeManagerTests : TestBaseClass<GraphTypeManager>
    {
        public GraphTypeManagerTests()
        {
            TestObject = new GraphTypeManager();
        }
    }
}