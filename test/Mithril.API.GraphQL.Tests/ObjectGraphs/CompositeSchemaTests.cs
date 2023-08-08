using Mithril.API.Abstractions.Query.Interfaces;
using Mithril.API.GraphQL.ObjectGraphs;
using Mithril.Tests.Helpers;

namespace Mithril.API.GraphQL.Tests.ObjectGraphs
{
    /// <summary>
    /// Composite schema tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;CompositeQuery&gt;"/>
    internal class CompositeSchemaTests : TestBaseClass<CompositeSchema>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeSchemaTests"/> class.
        /// </summary>
        public CompositeSchemaTests()
        {
            TestObject = new CompositeSchema(Array.Empty<IQuery>());
            ObjectType = typeof(CompositeSchema);
        }
    }
}