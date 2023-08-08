using Mithril.Mvc.Services;
using Mithril.Tests.Helpers;

namespace Mithril.Mvc.Tests.Services
{
    /// <summary>
    /// View renderer service tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;ViewRendererService&gt;"/>
    public class ViewRendererServiceTests : TestBaseClass<ViewRendererService>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewRendererServiceTests"/> class.
        /// </summary>
        public ViewRendererServiceTests()
        {
            TestObject = new ViewRendererService(null, null, null, null, null, null);
            ObjectType = typeof(ViewRendererService);
        }
    }
}