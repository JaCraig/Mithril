using Mithril.Tests.Helpers;
using Mithril.Themes.Abstractions.TagHelpers;

namespace Mithril.Themes.Abstractions.Tests.TagHelpers
{
    /// <summary>
    /// StyleTagHelper tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;StyleTagHelper&gt;" />
    public class StyleTagHelperTests : TestBaseClass<StyleTagHelper>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StyleTagHelperTests"/> class.
        /// </summary>
        public StyleTagHelperTests()
        {
            TestObject = new StyleTagHelper(null);
            ObjectType = typeof(StyleTagHelper);
        }
    }
}