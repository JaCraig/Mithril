using Inflatable.BaseClasses;
using Mithril.Core.Abstractions.Data.Interfaces;
using Mithril.Data.Inflatable.Databases;

namespace Mithril.Core.Models.Mappings
{
    /// <summary>
    /// Typed model mapping
    /// </summary>
    /// <seealso cref="Inflatable.BaseClasses.MappingBaseClass{ITypedModel, DefaultDatabase}"/>
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