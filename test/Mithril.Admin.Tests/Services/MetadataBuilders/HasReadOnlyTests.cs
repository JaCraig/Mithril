using Mithril.Admin.Services.MetadataBuilders;
using Mithril.Tests.Helpers;

namespace Mithril.Admin.Tests.Services.MetadataBuilders
{
    /// <summary>
    /// Has read only tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;HasReadOnly&gt;" />
    public class HasReadOnlyTests : TestBaseClass<HasReadOnly>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HasReadOnlyTests"/> class.
        /// </summary>
        public HasReadOnlyTests()
        {
            TestObject = new HasReadOnly();
        }
    }
}