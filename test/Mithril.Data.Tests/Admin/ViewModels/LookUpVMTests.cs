using Mithril.Data.Admin.ViewModels;
using Mithril.Tests.Helpers;

namespace Mithril.Data.Tests.Admin.ViewModels
{
    /// <summary>
    /// LookUpVM Tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;LookUpVM&gt;" />
    public class LookUpVMTests : TestBaseClass<LookUpVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LookUpVMTests"/> class.
        /// </summary>
        public LookUpVMTests()
        {
            TestObject = new LookUpVM();
            ObjectType = typeof(LookUpVM);
        }
    }
}