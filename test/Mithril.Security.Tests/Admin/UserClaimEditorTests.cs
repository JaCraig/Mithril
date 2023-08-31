using Mithril.Security.Admin;
using Mithril.Tests.Helpers;

namespace Mithril.Security.Tests.Admin
{
    /// <summary>
    /// User claim editor tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;UserClaimEditor&gt;" />
    public class UserClaimEditorTests : TestBaseClass<UserClaimEditor>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserClaimEditorTests"/> class.
        /// </summary>
        public UserClaimEditorTests()
        {
            TestObject = new UserClaimEditor(null, null, null);
            ObjectType = typeof(UserClaimEditor);
        }
    }
}