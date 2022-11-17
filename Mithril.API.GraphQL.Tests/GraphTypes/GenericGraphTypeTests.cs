using Mithril.API.GraphQL.GraphTypes;
using Mithril.Tests.Helpers;

namespace Mithril.API.GraphQL.Tests.GraphTypes
{
    public class GenericGraphTypeTests : TestBaseClass<GenericGraphType<TestClass>>
    {
        public GenericGraphTypeTests()
        {
            TestObject = new GenericGraphType<TestClass>();
        }
    }

    public class TestClass
    {
        public string Id { get; set; }
        public int SomethingElse { get; set; }
    }
}