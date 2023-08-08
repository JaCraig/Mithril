using Mithril.Tests.Helpers;
using Mithril.Themes.Abstractions.TagHelpers;

namespace Mithril.Themes.Abstractions.Tests.TagHelpers
{
    /// <summary>
    /// LinkTagHelper tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;LinkTagHelper&gt;" />
    public class LinkTagHelperTests : TestBaseClass<LinkTagHelper>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LinkTagHelperTests"/> class.
        /// </summary>
        public LinkTagHelperTests()
        {
            TestObject = new LinkTagHelper(null);
            ObjectType = typeof(LinkTagHelper);
        }
    }
}