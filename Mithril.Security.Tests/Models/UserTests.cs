using Mithril.Security.Models;
using Mithril.Tests.Helpers;

namespace Mithril.Security.Tests.Models
{
    public class UserTests : TestBaseClass<User>
    {
        public UserTests()
        {
            TestObject = new User();
            ObjectType = typeof(User);
        }
    }
}