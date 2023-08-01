using Mithril.Security.Admin;
using Mithril.Tests.Helpers;

namespace Mithril.Security.Tests.Admin
{
    /// <summary>
    /// Permission Editor Tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;PermissionEditor&gt;" />
    public class PermissionEditorTests : TestBaseClass<PermissionEditor>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionEditorTests"/> class.
        /// </summary>
        public PermissionEditorTests()
        {
            TestObject = new PermissionEditor(null, null);
            ObjectType = typeof(PermissionEditor);
        }
    }
}