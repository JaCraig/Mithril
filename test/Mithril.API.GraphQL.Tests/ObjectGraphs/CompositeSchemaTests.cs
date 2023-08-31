using Mithril.API.Abstractions.Query.Interfaces;
using Mithril.API.GraphQL.ObjectGraphs;
using Mithril.Tests.Helpers;
using Xunit;

namespace Mithril.API.GraphQL.Tests.ObjectGraphs
{
    /// <summary>
    /// Composite schema tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;CompositeQuery&gt;"/>
    public class CompositeSchemaTests : TestBaseClass<CompositeSchema>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeSchemaTests"/> class.
        /// </summary>
        public CompositeSchemaTests()
        {
            TestObject = new CompositeSchema(Array.Empty<IQuery>());
            ObjectType = typeof(CompositeSchema);
        }

        /// <summary>
        /// CompositeSchema constructor.
        /// </summary>
        [Fact]
        public void CompositeSchema_Constructor()
        {
            Assert.NotNull(TestObject);
        }

        /// <summary>
        /// CompositeSchema constructor with null queries.
        /// </summary>
        [Fact]
        public void CompositeSchema_Constructor_WithNullQueries()
        {
            Assert.NotNull(new CompositeSchema(null));
        }

        /// <summary>
        /// CompositeSchema constructor with queries.
        /// </summary>
        [Fact]
        public void CompositeSchema_Constructor_WithQueries()
        {
            Assert.NotNull(new CompositeSchema(new IQuery[] { }));
        }

        /// <summary>
        /// CompositeSchema constructor with queries and null.
        /// </summary>
        [Fact]
        public void CompositeSchema_Constructor_WithQueriesAndNull()
        {
            Assert.NotNull(new CompositeSchema(new IQuery?[] { null }));
        }
    }
}