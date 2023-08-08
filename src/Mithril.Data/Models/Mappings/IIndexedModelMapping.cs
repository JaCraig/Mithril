using Inflatable.BaseClasses;
using Mithril.Data.Abstractions.Databases;
using Mithril.Data.Abstractions.Interfaces;

namespace Mithril.Data.Models.Mappings
{
    /// <summary>
    /// IIndexedModel mapping
    /// </summary>
    /// <seealso cref="MappingBaseClass{IIndexedModel, DefaultDatabase}"/>
    public class IIndexedModelMapping : MappingBaseClass<IIndexedModel, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IIndexedModelMapping"/> class.
        /// </summary>
        public IIndexedModelMapping()
            : base(merge: true)
        {
        }
    }
}