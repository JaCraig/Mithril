using Mithril.Navigation.Services;
using Mithril.Tests.Helpers;

namespace Mithril.Navigation.Tests.Services
{
    /// <summary>
    /// MenuService tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;MenuService&gt;" />
    public class MenuServiceTests : TestBaseClass<MenuService>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuServiceTests"/> class.
        /// </summary>
        public MenuServiceTests()
        {
            TestObject = new MenuService(null, null);
            ObjectType = typeof(MenuService);
        }
    }
}