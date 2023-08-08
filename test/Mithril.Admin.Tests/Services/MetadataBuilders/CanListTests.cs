using Mithril.Admin.Services.MetadataBuilders;
using Mithril.Tests.Helpers;

namespace Mithril.Admin.Tests.Services.MetadataBuilders
{
    /// <summary>
    /// Can list tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;CanList&gt;" />
    public class CanListTests : TestBaseClass<CanList>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CanListTests"/> class.
        /// </summary>
        public CanListTests()
        {
            TestObject = new CanList();
        }
    }
}