using Mithril.API.GraphQL.Authorization;
using Mithril.Tests.Helpers;

namespace Mithril.API.GraphQL.Tests.Authorization
{
    public class GraphQLUserContextBuilderTests : TestBaseClass<GraphQLUserContextBuilder>
    {
        public GraphQLUserContextBuilderTests()
        {
            TestObject = new GraphQLUserContextBuilder();
            ObjectType = typeof(GraphQLUserContextBuilder);
        }
    }
}