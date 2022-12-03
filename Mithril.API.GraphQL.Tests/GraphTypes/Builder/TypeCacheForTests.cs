using Mithril.API.GraphQL.GraphTypes.Builder;
using Mithril.Tests.Helpers;

namespace Mithril.API.GraphQL.Tests.GraphTypes.Builder
{
    public class TypeCacheForTests : TestBaseClass
    {
        protected override Type? ObjectType { get; set; } = typeof(TypeCacheFor<TestClass>);
    }

    internal class TestClass
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public string TypeDescription { get; set; }
        public string TypeName { get; set; }

        public int Something(int x)
        { return x; }
    }
}