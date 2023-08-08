using Mithril.Features.Models;
using Mithril.Tests.Helpers;

namespace Mithril.Features.Tests.Models
{
    public class FeatureToggledEventTests : TestBaseClass<FeatureToggledEvent>
    {
        public FeatureToggledEventTests()
        {
            TestObject = new FeatureToggledEvent();
            ObjectType = typeof(FeatureToggledEvent);
        }
    }
}