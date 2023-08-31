using Mithril.Tests.Helpers;
using Mithril.Themes.Admin.DropDowns;
using Xunit;

namespace Mithril.Themes.Tests.Admin.DropDowns
{
    /// <summary>
    /// Theme list tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;ThemeList&gt;" />
    public class ThemeListTests : TestBaseClass<ThemeList>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThemeListTests"/> class.
        /// </summary>
        public ThemeListTests()
        {
            TestObject = new ThemeList();
        }

        /// <summary>
        /// ThemeList constructor.
        /// </summary>
        [Fact]
        public void ThemeList_Constructor()
        {
            Assert.NotNull(new ThemeList());
        }
    }
}