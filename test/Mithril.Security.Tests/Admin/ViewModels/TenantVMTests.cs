using Mithril.Security.Admin.ViewModels;
using Mithril.Tests.Helpers;

namespace Mithril.Security.Tests.Admin.ViewModels
{
    /// <summary>
    /// Tenant VM Tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;TenantVM&gt;" />
    public class TenantVMTests : TestBaseClass<TenantVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TenantVMTests"/> class.
        /// </summary>
        public TenantVMTests()
        {
            TestObject = new TenantVM();
            ObjectType = typeof(TenantVM);
        }
    }
}