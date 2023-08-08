using Mithril.Navigation.Queries;
using Mithril.Tests.Helpers;

namespace Mithril.Navigation.Tests.Queries
{
    /// <summary>
    /// MenuQuery tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;MenuQuery&gt;" />
    public class MenuQueryTests : TestBaseClass<MenuQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuQueryTests"/> class.
        /// </summary>
        public MenuQueryTests()
        {
            TestObject = new MenuQuery(null, null, null);
            ObjectType = typeof(MenuQuery);
        }
    }
}