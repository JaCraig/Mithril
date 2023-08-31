using Mithril.API.Abstractions.Admin.DropDowns;
using Mithril.Tests.Helpers;

namespace Mithril.API.Abstractions.Tests.Admin.DropDowns
{
    /// <summary>
    /// Look up drop down tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;LookUpTypeDropDown&gt;" />
    public class LookUpTypeDropDownTests : TestBaseClass<LookUpTypeDropDown>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LookUpTypeDropDownTests"/> class.
        /// </summary>
        public LookUpTypeDropDownTests()
        {
            TestObject = new LookUpTypeDropDown();
        }
    }
}