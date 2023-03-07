using Mithril.Routing.Models;
using Mithril.Tests.Helpers;

namespace Mithril.Routing.Tests.Models
{
    /// <summary>
    /// RouteEntry tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;RouteEntry&gt;"/>
    public class RouteEntryTests : TestBaseClass<RouteEntry>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RouteEntryTests"/> class.
        /// </summary>
        public RouteEntryTests()
        {
            TestObject = new RouteEntry();
            ObjectType = typeof(RouteEntry);
        }
    }
}