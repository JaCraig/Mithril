using Mithril.Features.Queries;
using Mithril.Tests.Helpers;

namespace Mithril.Features.Tests.Queries
{
    public class FeaturesQueryTests : TestBaseClass<FeaturesQuery>
    {
        public FeaturesQueryTests()
        {
            TestObject = new FeaturesQuery(null, null, null);
            ObjectType = typeof(FeaturesQuery);
        }
    }
}