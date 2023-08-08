using Mithril.Tests.Helpers;
using Mithril.Themes.Services;

namespace Mithril.Themes.Tests.Services
{
    /// <summary>
    /// ResourceService tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;ResourceService&gt;" />
    public class ResourceServiceTests : TestBaseClass<ResourceService>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceServiceTests"/> class.
        /// </summary>
        public ResourceServiceTests()
        {
            TestObject = new ResourceService();
        }
    }
}