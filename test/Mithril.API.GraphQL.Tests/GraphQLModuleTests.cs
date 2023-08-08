using Mithril.Tests.Helpers;

namespace Mithril.API.GraphQL.Tests
{
    public class GraphQLModuleTests : TestBaseClass<GraphQLModule>
    {
        public GraphQLModuleTests()
        {
            TestObject = new GraphQLModule();
            DiscoverInheritedMethods = true;
        }
    }
}