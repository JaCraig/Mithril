using Mithril.Navigation.Features;
using Mithril.Tests.Helpers;

namespace Mithril.Navigation.Tests.Features
{
    /// <summary>
    /// Navigation feature tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;NavigationFeature&gt;" />
    public class NavigationFeatureTests : TestBaseClass<NavigationFeature>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationFeatureTests"/> class.
        /// </summary>
        public NavigationFeatureTests()
        {
            TestObject = new NavigationFeature();
        }
    }
}