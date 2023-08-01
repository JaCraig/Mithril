using Mithril.Security.Admin.ViewModels;
using Mithril.Tests.Helpers;

namespace Mithril.Security.Tests.Admin.ViewModels
{
    /// <summary>
    /// Claim Drop Down VM Tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;ClaimDropDownVM&gt;" />
    public class ClaimDropDownVMTests : TestBaseClass<ClaimDropDownVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimDropDownVMTests"/> class.
        /// </summary>
        public ClaimDropDownVMTests()
        {
            TestObject = new ClaimDropDownVM();
            ObjectType = typeof(ClaimDropDownVM);
        }
    }
}