using Mithril.Security.Services;
using Mithril.Tests.Helpers;

namespace Mithril.Security.Tests.Services
{
    public class UserClaimsTransformerTests : TestBaseClass<UserClaimsTransformer>
    {
        public UserClaimsTransformerTests()
        {
            TestObject = new UserClaimsTransformer(null);
            ObjectType = typeof(UserClaimsTransformer);
        }
    }
}