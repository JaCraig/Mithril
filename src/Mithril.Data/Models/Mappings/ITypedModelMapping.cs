using Inflatable.BaseClasses;
using Mithril.Data.Abstractions.Databases;
using Mithril.Data.Abstractions.Interfaces;

namespace Mithril.Data.Models.Mappings
{
    /// <summary>
    /// Typed model mapping
    /// </summary>
    /// <seealso cref="MappingBaseClass{ITypedModel, DefaultDatabase}"/>
    public class ITypedModelMapping : MappingBaseClass<ITypedModel, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ITypedModelMapping"/> class.
        /// </summary>
        public ITypedModelMapping()
            : base(merge: true)
        {
            Reference(x => x.Type);
        }
    }
}