using Mithril.Admin.Abstractions.DataEditor.Attributes;
using Mithril.Tests.Helpers;

namespace Mithril.Admin.Abstractions.Tests.DataEditor.Attributes
{
    /// <summary>
    /// Query attribute tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;QueryAttribute&gt;" />
    public class QueryAttributeTests : TestBaseClass<QueryAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryAttributeTests"/> class.
        /// </summary>
        public QueryAttributeTests()
        {
            TestObject = new QueryAttribute(typeof(QueryAttribute), "test");
            ObjectType = typeof(QueryAttribute);
        }
    }
}