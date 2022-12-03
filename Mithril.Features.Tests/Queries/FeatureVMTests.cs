using Mithril.Features.Queries;
using Mithril.Tests.Helpers;

namespace Mithril.Features.Tests.Queries
{
    public class FeatureVMTests : TestBaseClass<FeatureVM>
    {
        public FeatureVMTests()
        {
            TestObject = new FeatureVM(new Features.Models.Feature());
            ObjectType = typeof(FeatureVM);
        }
    }
}