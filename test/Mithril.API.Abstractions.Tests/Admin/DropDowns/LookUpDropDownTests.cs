using Mithril.API.Abstractions.Admin.DropDowns;
using Mithril.Tests.Helpers;

namespace Mithril.API.Abstractions.Tests.Admin.DropDowns
{
    /// <summary>
    /// Look up drop down tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;LookUpDropDown&gt;" />
    public class LookUpDropDownTests : TestBaseClass<LookUpDropDown>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LookUpDropDownTests"/> class.
        /// </summary>
        public LookUpDropDownTests()
        {
            TestObject = new LookUpDropDown();
        }
    }
}