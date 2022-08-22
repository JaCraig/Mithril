using Inflatable.BaseClasses;
using Mithril.Core.Abstractions.Data.Models;
using Mithril.Data.Inflatable.Databases;

namespace Mithril.Core.Models.Mappings
{
    /// <summary>
    /// LookUp mapping
    /// </summary>
    /// <seealso cref="MappingBaseClass&lt;LookUp, DefaultDatabase&gt;"/>
    public class LookUpMapping : MappingBaseClass<LookUp, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LookUpMapping"/> class.
        /// </summary>
        public LookUpMapping()
            : base(merge: true)
        {
            Reference(x => x.DisplayName).WithDefaultValue(() => "");
            Reference(x => x.Icon).WithDefaultValue(() => "fa-info-circle");
            ManyToOne(x => x.Type);
        }
    }
}