using Inflatable.BaseClasses;
using Mithril.Data.Abstractions.Databases;
using Mithril.Data.Abstractions.Interfaces;

namespace Mithril.Data.Models.General.Mappings
{
    /// <summary>
    /// ILookUpType mapping
    /// </summary>
    /// <seealso cref="MappingBaseClass{ILookUpType, DefaultDatabase}"/>
    public class ILookUpTypeMapping : MappingBaseClass<ILookUpType, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ILookUpTypeMapping"/> class.
        /// </summary>
        public ILookUpTypeMapping()
            : base(merge: true)
        {
            _ = Reference(x => x.Description).WithDefaultValue(() => "");
            _ = Reference(x => x.DisplayName).WithDefaultValue(() => "").IsUnique();
            _ = ManyToOne(x => x.LookUps).CascadeChanges();
        }
    }
}