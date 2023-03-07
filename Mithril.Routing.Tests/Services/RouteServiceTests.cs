using Mithril.Routing.Services;
using Mithril.Tests.Helpers;

namespace Mithril.Routing.Tests.Services
{
    /// <summary>
    /// RouteService tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;RouteService&gt;"/>
    public class RouteServiceTests : TestBaseClass<RouteService>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RouteServiceTests"/> class.
        /// </summary>
        public RouteServiceTests()
        {
            TestObject = new RouteService(null);
            ObjectType = typeof(RouteService);
        }
    }
}