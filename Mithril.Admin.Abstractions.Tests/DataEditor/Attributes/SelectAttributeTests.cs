using Mithril.Admin.Abstractions.DataEditor.Attributes;
using Mithril.Tests.Helpers;

namespace Mithril.Admin.Abstractions.Tests.DataEditor.Attributes
{
    /// <summary>
    /// Select attribute tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;SelectAttribute&gt;" />
    public class SelectAttributeTests : TestBaseClass<SelectAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectAttributeTests" /> class.
        /// </summary>
        public SelectAttributeTests()
        {
            TestObject = new SelectAttribute(typeof(SelectAttribute), "test");
            ObjectType = typeof(SelectAttribute);
        }
    }
}