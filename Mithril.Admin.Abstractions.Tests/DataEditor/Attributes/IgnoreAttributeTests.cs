using Mithril.Admin.Abstractions.DataEditor.Attributes;
using Mithril.Tests.Helpers;

namespace Mithril.Admin.Abstractions.Tests.DataEditor.Attributes
{
    /// <summary>
    /// Ignore attribute tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;IgnoreAttribute&gt;" />
    public class IgnoreAttributeTests : TestBaseClass<IgnoreAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IgnoreAttributeTests"/> class.
        /// </summary>
        public IgnoreAttributeTests()
        {
            TestObject = new IgnoreAttribute();
        }
    }
}