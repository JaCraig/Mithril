using Mithril.Features.Models;
using Mithril.Tests.Helpers;

namespace Mithril.Features.Tests.Models
{
    public class FeatureTests : TestBaseClass<Feature>
    {
        public FeatureTests()
        {
            TestObject = new Feature();
            ObjectType = typeof(Feature);
        }
    }
}