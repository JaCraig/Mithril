using Mithril.Tests.Helpers;
using Mithril.Themes.Abstractions.BaseClasses;

namespace Mithril.Themes.Abstractions.Tests.BaseClasses
{
    /// <summary>
    /// Theme base class tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;TestTheme&gt;" />
    public class ThemeBaseClassTests : TestBaseClass<TestTheme>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThemeBaseClassTests"/> class.
        /// </summary>
        public ThemeBaseClassTests()
        {
            TestObject = new TestTheme();
            DiscoverInheritedMethods = true;
        }
    }

    /// <summary>
    /// Test theme
    /// </summary>
    /// <seealso cref="ThemeBaseClass&lt;TestTheme&gt;" />
    public class TestTheme : ThemeBaseClass<TestTheme>
    { }
}