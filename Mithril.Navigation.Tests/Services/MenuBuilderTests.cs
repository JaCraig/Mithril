using Mithril.Navigation.Services;
using Mithril.Tests.Helpers;

namespace Mithril.Navigation.Tests.Services
{
    /// <summary>
    /// MenuBuilder tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;MenuBuilder&gt;" />
    public class MenuBuilderTests : TestBaseClass<MenuBuilder>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuBuilderTests"/> class.
        /// </summary>
        public MenuBuilderTests()
        {
            TestObject = new MenuBuilder("", null, null);
            ObjectType = typeof(MenuBuilder);
        }
    }
}