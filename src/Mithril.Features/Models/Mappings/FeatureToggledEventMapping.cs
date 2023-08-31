using Inflatable.BaseClasses;
using Mithril.Data.Abstractions.Databases;

namespace Mithril.Features.Models.Mappings
{
    /// <summary>
    /// Feature toggled event mapping
    /// </summary>
    /// <seealso cref="MappingBaseClass&lt;FeatureToggledEvent, DefaultDatabase&gt;"/>
    public class FeatureToggledEventMapping : MappingBaseClass<FeatureToggledEvent, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureMapping"/> class.
        /// </summary>
        public FeatureToggledEventMapping()
        {
            _ = Reference(x => x.FeatureName);
            _ = Reference(x => x.FeatureStatus);
        }
    }
}