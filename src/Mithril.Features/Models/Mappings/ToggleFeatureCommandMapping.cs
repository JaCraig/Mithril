using Inflatable.BaseClasses;
using Mithril.Data.Abstractions.Databases;

namespace Mithril.Features.Models.Mappings
{
    /// <summary>
    /// Toggle feature command mapping
    /// </summary>
    /// <seealso cref="MappingBaseClass&lt;ToggleFeatureCommand, DefaultDatabase&gt;"/>
    public class ToggleFeatureCommandMapping : MappingBaseClass<ToggleFeatureCommand, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToggleFeatureCommandMapping"/> class.
        /// </summary>
        public ToggleFeatureCommandMapping()
        {
            _ = Reference(x => x.FeatureName);
            _ = Reference(x => x.FeatureStatus);
        }
    }
}