using Mithril.Navigation.Models;
using Mithril.Tests.Helpers;

namespace Mithril.Navigation.Tests.Models
{
    /// <summary>
    /// Menu tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;Menu&gt;" />
    public class MenuTests : TestBaseClass<Menu>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuTests"/> class.
        /// </summary>
        public MenuTests()
        {
            TestObject = new Menu();
            ObjectType = typeof(Menu);
        }
    }
}