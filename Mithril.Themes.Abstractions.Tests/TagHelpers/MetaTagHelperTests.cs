using Mithril.Tests.Helpers;
using Mithril.Themes.Abstractions.TagHelpers;

namespace Mithril.Themes.Abstractions.Tests.TagHelpers
{
    /// <summary>
    /// MetaTagHelper tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;MetaTagHelper&gt;" />
    public class MetaTagHelperTests : TestBaseClass<MetaTagHelper>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MetaTagHelperTests"/> class.
        /// </summary>
        public MetaTagHelperTests()
        {
            TestObject = new MetaTagHelper(null);
            ObjectType = typeof(MetaTagHelper);
        }
    }
}