using Mithril.Tests.Helpers;
using Mithril.Themes.Abstractions.Interfaces;
using Mithril.Themes.Services;

namespace Mithril.Themes.Tests.Services
{
    /// <summary>
    /// ThemeService tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;ThemeService&gt;" />
    public class ThemeServiceTests : TestBaseClass<ThemeService>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThemeServiceTests"/> class.
        /// </summary>
        public ThemeServiceTests()
        {
            TestObject = new ThemeService(Array.Empty<ITheme>(), null);
            ObjectType = typeof(ThemeService);
        }
    }
}