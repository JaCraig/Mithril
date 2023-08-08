using Mithril.Security.Services;
using Mithril.Tests.Helpers;

namespace Mithril.Security.Tests.Services
{
    public class SecurityServiceTests : TestBaseClass<SecurityService>
    {
        public SecurityServiceTests()
        {
            TestObject = new SecurityService(null, null);
            ObjectType = typeof(SecurityService);
        }
    }
}