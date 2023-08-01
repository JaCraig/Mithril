using Mithril.Admin.Abstractions.DataEditor.Attributes;
using Mithril.Tests.Helpers;

namespace Mithril.Admin.Abstractions.Tests.DataEditor.Attributes
{
    /// <summary>
    /// Display as text attribute tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;DisplayAsTextAttribute&gt;" />
    public class DisplayAsTextAttributeTests : TestBaseClass<DisplayAsTextAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayAsTextAttributeTests"/> class.
        /// </summary>
        public DisplayAsTextAttributeTests()
        {
            TestObject = new DisplayAsTextAttribute();
        }
    }
}