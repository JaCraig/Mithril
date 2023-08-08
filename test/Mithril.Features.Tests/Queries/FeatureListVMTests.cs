using Mithril.Features.Models;
using Mithril.Features.Queries;
using Mithril.Tests.Helpers;

namespace Mithril.Features.Tests.Queries
{
    /// <summary>
    /// FeatureListVM tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;FeatureListVM&gt;"/>
    public class FeatureListVMTests : TestBaseClass<FeatureListVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureListVMTests"/> class.
        /// </summary>
        public FeatureListVMTests()
        {
            TestObject = new FeatureListVM(Array.Empty<Feature>());
            ObjectType = typeof(FeatureListVM);
        }
    }
}