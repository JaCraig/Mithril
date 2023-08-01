using Mithril.Tests.Helpers;
using Mithril.Themes.Admin;

namespace Mithril.Themes.Tests.Admin
{
    /// <summary>
    /// Theme Editor Tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;ThemeEditor&gt;" />
    public class ThemeEditorTests : TestBaseClass<ThemeEditor>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThemeEditorTests"/> class.
        /// </summary>
        public ThemeEditorTests()
        {
            TestObject = new ThemeEditor(null, null, null);
            ObjectType = typeof(ThemeEditor);
        }
    }
}