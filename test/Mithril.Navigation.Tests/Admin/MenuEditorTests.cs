using Mithril.Navigation.Admin;
using Mithril.Tests.Helpers;

namespace Mithril.Navigation.Tests.Admin
{
    /// <summary>
    /// Menu editor tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;MenuEditor&gt;" />
    public class MenuEditorTests : TestBaseClass<MenuEditor>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuEditorTests"/> class.
        /// </summary>
        public MenuEditorTests()
        {
            TestObject = new MenuEditor(null, null, null);
            ObjectType = typeof(MenuEditor);
        }
    }
}