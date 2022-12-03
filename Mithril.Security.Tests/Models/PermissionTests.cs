using Mithril.Security.Models;
using Mithril.Tests.Helpers;

namespace Mithril.Security.Tests.Models
{
    public class PermissionTests : TestBaseClass<Permission>
    {
        public PermissionTests()
        {
            TestObject = new Permission();
            ObjectType = typeof(Permission);
        }
    }
}