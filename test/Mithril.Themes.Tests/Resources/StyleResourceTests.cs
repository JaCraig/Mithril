using Mithril.Tests.Helpers;
using Mithril.Themes.Resources;

namespace Mithril.Themes.Tests.Resources
{
    /// <summary>
    /// StyleResource tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;StyleResource&gt;" />
    public class StyleResourceTests : TestBaseClass<StyleResource>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StyleResourceTests"/> class.
        /// </summary>
        public StyleResourceTests()
        {
            TestObject = new StyleResource("", "", "", 0, "");
            ObjectType = typeof(StyleResource);
        }
    }
}