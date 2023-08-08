using Mithril.Security.Admin.ViewModels;
using Mithril.Tests.Helpers;

namespace Mithril.Security.Tests.Admin.ViewModels
{
    /// <summary>
    /// Permission VM Tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;PermissionVM&gt;" />
    public class PermissionVMTests : TestBaseClass<PermissionVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionVMTests"/> class.
        /// </summary>
        public PermissionVMTests()
        {
            TestObject = new PermissionVM();
            ObjectType = typeof(PermissionVM);
        }
    }
}