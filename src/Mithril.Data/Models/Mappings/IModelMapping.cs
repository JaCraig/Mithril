using Inflatable.BaseClasses;
using Mithril.Data.Abstractions.Databases;
using Mithril.Data.Abstractions.Interfaces;

namespace Mithril.Data.Models.Mappings
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
            _ = ID(x => x.ID).IsAutoIncremented();
            _ = Reference(x => x.Active).WithDefaultValue(() => true);
            _ = Reference(x => x.DateCreated).WithDefaultValue(() => new DateTime(1900, 1, 1));
            _ = Reference(x => x.DateModified).WithDefaultValue(() => new DateTime(1900, 1, 1));
            _ = Map(x => x.Creator);
            _ = Map(x => x.Modifier);
            _ = Reference(x => x.TenantID);
        }
    }
}