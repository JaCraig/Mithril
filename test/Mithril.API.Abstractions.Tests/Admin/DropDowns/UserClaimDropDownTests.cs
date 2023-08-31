using Mithril.API.Abstractions.Admin.DropDowns;
using Mithril.Tests.Helpers;

namespace Mithril.API.Abstractions.Tests.Admin.DropDowns
{
    /// <summary>
    /// User claim drop down tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;UserClaimDropDown&gt;" />
    public class UserClaimDropDownTests : TestBaseClass<UserClaimDropDown>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserClaimDropDownTests"/> class.
        /// </summary>
        public UserClaimDropDownTests()
        {
            TestObject = new UserClaimDropDown();
        }

        [Fact]
        public void UserClaimDropDown_Constructor()
        {
            Assert.NotNull(new UserClaimDropDown());
        }
    }
}