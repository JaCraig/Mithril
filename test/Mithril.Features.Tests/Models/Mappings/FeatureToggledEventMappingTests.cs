using Mithril.Features.Models.Mappings;
using Mithril.Tests.Helpers;

namespace Mithril.Features.Tests.Models.Mappings
{
    /// <summary>
    /// FeatureToggledEventMapping tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;FeatureToggledEventMapping&gt;"/>
    public class FeatureToggledEventMappingTests : TestBaseClass<FeatureToggledEventMapping>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureToggledEventMappingTests"/> class.
        /// </summary>
        public FeatureToggledEventMappingTests()
        {
            TestObject = new FeatureToggledEventMapping();
        }
    }
}