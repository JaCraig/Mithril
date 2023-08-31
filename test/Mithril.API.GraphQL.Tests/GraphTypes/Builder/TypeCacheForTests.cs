using Mithril.API.GraphQL.GraphTypes.Builder;
using Mithril.Tests.Helpers;

namespace Mithril.API.GraphQL.Tests.GraphTypes.Builder
{
    /// <summary>
    /// Type cache for tests
    /// </summary>
    /// <seealso cref="TestBaseClass"/>
    public class TypeCacheForTests : TestBaseClass
    {
        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        protected override Type? ObjectType { get; set; } = typeof(TypeCacheFor<TestClass>);
    }

    /// <summary>
    /// Test class
    /// </summary>
    internal class TestClass
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the type description.
        /// </summary>
        /// <value>The type description.</value>
        public string? TypeDescription { get; set; }

        /// <summary>
        /// Gets or sets the name of the type.
        /// </summary>
        /// <value>The name of the type.</value>
        public string? TypeName { get; set; }

        /// <summary>
        /// Somethings the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns></returns>
        public int Something(int x) => x;
    }
}