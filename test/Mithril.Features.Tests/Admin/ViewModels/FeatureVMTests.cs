using Mithril.Features.Admin.ViewModels;
using Mithril.Tests.Helpers;

namespace Mithril.Features.Tests.Admin.ViewModels
{
    /// <summary>
    /// Feature VM tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;FeatureVM&gt;" />
    public class FeatureVMTests : TestBaseClass<FeatureVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureVMTests"/> class.
        /// </summary>
        public FeatureVMTests()
        {
            TestObject = new FeatureVM();
            ObjectType = typeof(FeatureVM);
        }
    }
}