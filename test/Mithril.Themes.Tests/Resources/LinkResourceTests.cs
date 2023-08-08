using Mithril.Tests.Helpers;
using Mithril.Themes.Resources;

namespace Mithril.Themes.Tests.Resources
{
    /// <summary>
    /// LinkResource tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;LinkResource&gt;" />
    public class LinkResourceTests : TestBaseClass<LinkResource>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LinkResourceTests"/> class.
        /// </summary>
        public LinkResourceTests()
        {
            TestObject = new LinkResource("", "", "", "", "", "", "", "", "", "", 0, "");
            ObjectType = typeof(LinkResource);
        }
    }
}