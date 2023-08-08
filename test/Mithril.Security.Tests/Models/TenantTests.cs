using Mithril.Security.Models;
using Mithril.Tests.Helpers;

namespace Mithril.Security.Tests.Models
{
    public class TenantTests : TestBaseClass<Tenant>
    {
        public TenantTests()
        {
            TestObject = new Tenant();
            ObjectType = typeof(Tenant);
        }
    }
}