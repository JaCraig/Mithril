using Inflatable.BaseClasses;
using Mithril.Core.Abstractions.Data.Models;
using Mithril.Data.Inflatable.Databases;

namespace Mithril.Core.Models.Mappings
{
    /// <summary>
    /// LookUpType mapping
    /// </summary>
    /// <seealso cref="MappingBaseClass&lt;LookUpType, DefaultDatabase&gt;"/>
    public class LookUpTypeMapping : MappingBaseClass<LookUpType, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LookUpTypeMapping"/> class.
        /// </summary>
        public LookUpTypeMapping()
            : base(merge: true)
        {
            Reference(x => x.Description).WithDefaultValue(() => "");
            Reference(x => x.DisplayName).WithDefaultValue(() => "").IsUnique();
            ManyToOne(x => x.LookUps).CascadeChanges();
        }
    }
}