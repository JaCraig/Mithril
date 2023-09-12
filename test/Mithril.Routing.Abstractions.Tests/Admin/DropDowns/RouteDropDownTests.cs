using Mithril.Routing.Abstractions.Admin.DropDowns;
using Mithril.Tests.Helpers;

namespace Mithril.Routing.Abstractions.Tests.Admin.DropDowns
{
    /// <summary>
    /// Route drop down tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;RouteDropDown&gt;" />
    public class RouteDropDownTests : TestBaseClass<RouteDropDown>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RouteDropDownTests"/> class.
        /// </summary>
        public RouteDropDownTests()
        {
            TestObject = new RouteDropDown();
            ObjectType = typeof(RouteDropDown);
        }
    }
}