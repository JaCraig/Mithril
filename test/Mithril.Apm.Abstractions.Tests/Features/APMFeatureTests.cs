using Mithril.Apm.Abstractions.Features;
using Mithril.Tests.Helpers;

namespace Mithril.Apm.Abstractions.Tests.Features
{
    /// <summary>
    /// APM Feature tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;APMFeature&gt;"/>
    public class APMFeatureTests : TestBaseClass<APMFeature>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="APMFeatureTests"/> class.
        /// </summary>
        public APMFeatureTests()
        {
            TestObject = new APMFeature();
        }
    }
}