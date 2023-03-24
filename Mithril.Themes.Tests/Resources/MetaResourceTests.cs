using Mithril.Tests.Helpers;
using Mithril.Themes.Resources;

namespace Mithril.Themes.Tests.Resources
{
    /// <summary>
    /// MetaResource tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;MetaResource&gt;" />
    public class MetaResourceTests : TestBaseClass<MetaResource>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MetaResourceTests"/> class.
        /// </summary>
        public MetaResourceTests()
        {
            TestObject = new MetaResource("", "", "", "", "", "", 0);
            ObjectType = typeof(MetaResource);
        }
    }
}