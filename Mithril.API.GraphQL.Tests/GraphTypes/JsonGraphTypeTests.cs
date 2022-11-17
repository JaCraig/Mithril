using Mithril.API.GraphQL.GraphTypes;
using Mithril.Tests.Helpers;

namespace Mithril.API.GraphQL.Tests.GraphTypes
{
    public class JsonGraphTypeTests : TestBaseClass<JsonGraphType>
    {
        public JsonGraphTypeTests()
        {
            TestObject = new JsonGraphType();
        }
    }
}