using Mithril.Security.Models;
using Mithril.Tests.Helpers;

namespace Mithril.Security.Tests.Models
{
    internal class UserClaimTests : TestBaseClass<UserClaim>
    {
        public UserClaimTests()
        {
            TestObject = new UserClaim();
            ObjectType = typeof(UserClaim);
        }
    }
}