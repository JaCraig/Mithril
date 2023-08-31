using Mithril.Security.Admin;
using Mithril.Tests.Helpers;

namespace Mithril.Security.Tests.Admin
{
    /// <summary>
    /// User editor tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;UserEditor&gt;" />
    public class UserEditorTests : TestBaseClass<UserEditor>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserEditorTests"/> class.
        /// </summary>
        public UserEditorTests()
        {
            TestObject = new UserEditor(null, null, null);
            ObjectType = typeof(UserEditor);
        }
    }
}