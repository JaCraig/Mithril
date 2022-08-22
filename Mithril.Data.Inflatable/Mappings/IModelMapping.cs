using Inflatable.BaseClasses;
using Mithril.Core.Abstractions.Data.Interfaces;
using Mithril.Data.Inflatable.Databases;

namespace Mithril.Core.Models.Mappings
{
    /// <summary>
    /// IModel mapping
    /// </summary>
    /// <seealso cref="MappingBaseClass&lt;IModel, DefaultDatabase&gt;"/>
    public class IModelMapping : MappingBaseClass<IModel, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IModelMapping"/> class.
        /// </summary>
        public IModelMapping()
            : base(merge: true)
        {
            ID(x => x.ID).IsAutoIncremented();
            Reference(x => x.Active).WithDefaultValue(() => true);
            Reference(x => x.DateCreated).WithDefaultValue(() => new DateTime(1900, 1, 1));
            Reference(x => x.DateModified).WithDefaultValue(() => new DateTime(1900, 1, 1));
            Map(x => x.Creator);
            Map(x => x.Modifier);
        }
    }
}