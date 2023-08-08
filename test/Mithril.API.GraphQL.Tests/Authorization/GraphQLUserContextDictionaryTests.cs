using Mithril.API.GraphQL.Authorization;
using Mithril.Tests.Helpers;

namespace Mithril.API.GraphQL.Tests.Authorization
{
    public class GraphQLUserContextDictionaryTests : TestBaseClass<GraphQLUserContextDictionary>
    {
        public GraphQLUserContextDictionaryTests()
        {
            TestObject = new GraphQLUserContextDictionary(null);
            ObjectType = typeof(GraphQLUserContextDictionary);
        }
    }
}