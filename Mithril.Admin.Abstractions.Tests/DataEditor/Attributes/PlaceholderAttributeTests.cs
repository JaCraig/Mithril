using Mithril.Admin.Abstractions.DataEditor.Attributes;
using Mithril.Tests.Helpers;

namespace Mithril.Admin.Abstractions.Tests.DataEditor.Attributes
{
    /// <summary>
    /// Placeholder attribute tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;PlaceholderAttribute&gt;" />
    public class PlaceholderAttributeTests : TestBaseClass<PlaceholderAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaceholderAttributeTests"/> class.
        /// </summary>
        public PlaceholderAttributeTests()
        {
            TestObject = new PlaceholderAttribute("test");
            ObjectType = typeof(PlaceholderAttribute);
        }
    }
}