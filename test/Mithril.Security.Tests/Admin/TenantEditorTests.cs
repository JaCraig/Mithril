using Mithril.Security.Admin;
using Mithril.Tests.Helpers;

namespace Mithril.Security.Tests.Admin
{
    /// <summary>
    /// Tenant Editor Tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;TenantEditor&gt;" />
    public class TenantEditorTests : TestBaseClass<TenantEditor>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TenantEditorTests"/> class.
        /// </summary>
        public TenantEditorTests()
        {
            TestObject = new TenantEditor(null, null, null);
            ObjectType = typeof(TenantEditor);
        }
    }
}