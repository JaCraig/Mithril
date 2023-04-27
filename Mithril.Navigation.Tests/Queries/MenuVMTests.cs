using Mithril.Navigation.Queries;
using Mithril.Tests.Helpers;

namespace Mithril.Navigation.Tests.Queries
{
    /// <summary>
    /// MenuVM tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;MenuVM&gt;" />
    public class MenuVMTests : TestBaseClass<MenuVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuVMTests"/> class.
        /// </summary>
        public MenuVMTests()
        {
            TestObject = new MenuVM(null, null);
            ObjectType = typeof(MenuVM);
        }
    }
}