using Mithril.Security.Admin.DropDowns;
using Mithril.Tests.Helpers;

namespace Mithril.Security.Tests.Admin.DropDowns
{
    /// <summary>
    /// Claim types drop down tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;ClaimTypesDropDown&gt;" />
    public class ClaimTypesDropDownTests : TestBaseClass<ClaimTypesDropDown>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimTypesDropDownTests"/> class.
        /// </summary>
        public ClaimTypesDropDownTests()
        {
            TestObject = new ClaimTypesDropDown(null);
        }
    }
}