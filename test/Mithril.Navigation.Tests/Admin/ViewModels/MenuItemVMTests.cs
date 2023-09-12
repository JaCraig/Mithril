using Mithril.Navigation.Admin.ViewModels;
using Mithril.Tests.Helpers;

namespace Mithril.Navigation.Tests.Admin.ViewModels
{
    /// <summary>
    /// Menu item VM tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;MenuItemVM&gt;" />
    public class MenuItemVMTests : TestBaseClass<MenuItemVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItemVMTests"/> class.
        /// </summary>
        public MenuItemVMTests()
        {
            TestObject = new MenuItemVM();
            ObjectType = typeof(MenuItemVM);
        }
    }
}