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
            Reference(x => x.Category);
            Reference(x => x.Description);
            Reference(x => x.Name);
        }
    }
}