using Mithril.Security.Admin.DropDowns;
using Mithril.Tests.Helpers;

namespace Mithril.Security.Tests.Admin.DropDowns
{
    /// <summary>
    /// Tenant drop down tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;TenantDropDown&gt;" />
    public class TenantDropDownTests : TestBaseClass<TenantDropDown>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TenantDropDownTests"/> class.
        /// </summary>
        public TenantDropDownTests()
        {
            TestObject = new TenantDropDown();
        }
    }
}