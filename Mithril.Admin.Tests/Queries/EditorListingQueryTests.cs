using Mithril.Admin.Queries;
using Mithril.Tests.Helpers;

namespace Mithril.Admin.Tests.Queries
{
    /// <summary>
    /// Editor listing query tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;EditorListingQuery&gt;" />
    public class EditorListingQueryTests : TestBaseClass<EditorListingQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditorListingQueryTests"/> class.
        /// </summary>
        public EditorListingQueryTests()
        {
            TestObject = new EditorListingQuery(null, null, null);
            ObjectType = typeof(EditorListingQuery);
        }
    }
}