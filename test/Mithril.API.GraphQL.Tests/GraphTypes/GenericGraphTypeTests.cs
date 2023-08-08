using Mithril.API.GraphQL.GraphTypes;
using Mithril.Tests.Helpers;

namespace Mithril.API.GraphQL.Tests.GraphTypes
{
    /// <summary>
    /// Generic graph type tests
    /// </summary>
    /// <seealso cref="Mithril.Tests.Helpers.TestBaseClass&lt;Mithril.API.GraphQL.GraphTypes.GenericGraphType&lt;Mithril.API.GraphQL.Tests.GraphTypes.TestClass&gt;&gt;"/>
    public class GenericGraphTypeTests : TestBaseClass<GenericGraphType<TestClass>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericGraphTypeTests"/> class.
        /// </summary>
        public GenericGraphTypeTests()
        {
            TestObject = new GenericGraphType<TestClass>();
        }
    }

    /// <summary>
    /// Test class
    /// </summary>
    public class TestClass
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets something else.
        /// </summary>
        /// <value>Something else.</value>
        public int SomethingElse { get; set; }
    }
}