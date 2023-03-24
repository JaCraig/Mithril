using Mithril.Tests.Helpers;
using Mithril.Themes.Models;

namespace Mithril.Themes.Tests.Models
{
    /// <summary>
    /// Theme tests
    /// </summary>
    /// <seealso cref="Mithril.Tests.Helpers.TestBaseClass&lt;Mithril.Themes.Models.Theme&gt;" />
    public class ThemeTests : TestBaseClass<Theme>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThemeTests"/> class.
        /// </summary>
        public ThemeTests()
        {
            TestObject = new Theme("Test");
            ObjectType = typeof(Theme);
        }
    }
}