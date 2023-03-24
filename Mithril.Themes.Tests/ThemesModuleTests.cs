using Mithril.Tests.Helpers;

namespace Mithril.Themes.Tests
{
    /// <summary>
    /// Themes module tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;ThemesModule&gt;" />
    public class ThemesModuleTests : TestBaseClass<ThemesModule>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThemesModuleTests"/> class.
        /// </summary>
        public ThemesModuleTests()
        {
            TestObject = new ThemesModule();
            ObjectType = typeof(ThemesModule);
        }
    }
}