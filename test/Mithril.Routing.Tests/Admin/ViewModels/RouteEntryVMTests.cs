using Mithril.Routing.Admin.ViewModels;
using Mithril.Tests.Helpers;

namespace Mithril.Routing.Tests.Admin.ViewModels
{
    /// <summary>
    /// Route entry VM tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;RouteEntryVM&gt;" />
    public class RouteEntryVMTests : TestBaseClass<RouteEntryVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RouteEntryVMTests"/> class.
        /// </summary>
        public RouteEntryVMTests()
        {
            TestObject = new RouteEntryVM();
            ObjectType = typeof(RouteEntryVM);
        }
    }
}