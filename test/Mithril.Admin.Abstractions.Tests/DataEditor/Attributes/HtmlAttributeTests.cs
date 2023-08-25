using Mithril.Admin.Abstractions.DataEditor.Attributes;
using Mithril.Tests.Helpers;

namespace Mithril.Admin.Abstractions.Tests.DataEditor.Attributes
{
    /// <summary>
    /// HTML attribute
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;HtmlAttribute&gt;" />
    public class HtmlAttributeTests : TestBaseClass<HtmlAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlAttributeTests"/> class.
        /// </summary>
        public HtmlAttributeTests()
        {
            TestObject = new HtmlAttribute();
            ObjectType = typeof(HtmlAttribute);
        }
    }
}