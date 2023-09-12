using Mithril.Data.Admin.ViewModels;
using Mithril.Tests.Helpers;

namespace Mithril.Data.Tests.Admin.ViewModels
{
    /// <summary>
    /// LookUpType VM Tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;LookUpTypeVM&gt;" />
    public class LookUpTypeVMTests : TestBaseClass<LookUpTypeVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LookUpTypeVMTests"/> class.
        /// </summary>
        public LookUpTypeVMTests()
        {
            TestObject = new LookUpTypeVM();
            ObjectType = typeof(LookUpTypeVM);
        }
    }
}