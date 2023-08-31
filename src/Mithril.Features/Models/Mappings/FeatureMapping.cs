using Inflatable.BaseClasses;
using Mithril.Data.Abstractions.Databases;

namespace Mithril.Features.Models.Mappings
{
    /// <summary>
    /// Feature mapping
    /// </summary>
    /// <seealso cref="MappingBaseClass&lt;Feature, DefaultDatabase&gt;"/>
    public class FeatureMapping : MappingBaseClass<Feature, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureMapping"/> class.
        /// </summary>
        public FeatureMapping()
        {
            _ = Reference(x => x.Category);
            _ = Reference(x => x.Description);
            _ = Reference(x => x.Name);
        }
    }
}