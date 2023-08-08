using Mithril.Navigation.Queries;
using Mithril.Tests.Helpers;

namespace Mithril.Navigation.Tests.Queries
{
    /// <summary>
    /// MenuItemVM tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;MenuItemVM&gt;" />
    public class MenuItemVMTests : TestBaseClass<MenuItemVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItemVMTests"/> class.
        /// </summary>
        public MenuItemVMTests()
        {
            TestObject = new MenuItemVM(null);
            ObjectType = typeof(MenuItemVM);
        }
    }
}