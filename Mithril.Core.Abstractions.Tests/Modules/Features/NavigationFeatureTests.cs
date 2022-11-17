using Mithril.Core.Abstractions.Modules.Features;
using Mithril.Tests.Helpers;

namespace Mithril.Core.Abstractions.Tests.Modules.Features
{
    public class NavigationFeatureTests : TestBaseClass<NavigationFeature>
    {
        public NavigationFeatureTests()
        {
            TestObject = new NavigationFeature();
        }
    }
}