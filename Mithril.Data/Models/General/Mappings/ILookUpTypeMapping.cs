using Inflatable.BaseClasses;
using Mithril.Data.Abstractions.Interfaces;
using Mithril.Data.Inflatable.Databases;

namespace Mithril.Data.Inflatable.Models.General.Mappings
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
            Reference(x => x.Description).WithDefaultValue(() => "");
            Reference(x => x.DisplayName).WithDefaultValue(() => "").IsUnique();
            ManyToOne(x => x.LookUps).CascadeChanges();
        }
    }
}