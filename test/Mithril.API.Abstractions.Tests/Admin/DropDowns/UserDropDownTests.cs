using Mithril.API.Abstractions.Admin.DropDowns;
using Mithril.Tests.Helpers;
using Xunit;

namespace Mithril.API.Abstractions.Tests.Admin.DropDowns
{
    /// <summary>
    /// User Drop Down Tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;UserDropDown&gt;"/>
    public class UserDropDownTests : TestBaseClass<UserDropDown>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserDropDownTests"/> class.
        /// </summary>
        public UserDropDownTests()
        {
            TestObject = new UserDropDown();
        }

        /// <summary>
        /// Users the claim drop down constructor.
        /// </summary>
        [Fact]
        public void UserClaimDropDown_Constructor() => Assert.NotNull(new UserDropDown());
    }
}