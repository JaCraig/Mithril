using Mithril.Tests.Helpers;

namespace Mithril.Features.Tests
{
    public class FeatureModuleTests : TestBaseClass<FeatureModule>
    {
        public FeatureModuleTests()
        {
            TestObject = new FeatureModule();
            DiscoverInheritedMethods = true;
        }
    }
}