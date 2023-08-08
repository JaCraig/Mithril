using Mithril.API.Abstractions.Query.Interfaces;
using Mithril.API.GraphQL.GraphTypes;
using Mithril.Tests.Helpers;

namespace Mithril.API.GraphQL.Tests.GraphTypes
{
    /// <summary>
    /// Composite query tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;CompositeQuery&gt;"/>
    public class CompositeQueryTests : TestBaseClass<CompositeQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeQueryTests"/> class.
        /// </summary>
        public CompositeQueryTests()
        {
            TestObject = new CompositeQuery(Array.Empty<IQuery>());
            ObjectType = typeof(CompositeQuery);
        }
    }
}