using Inflatable.BaseClasses;
using Mithril.Core.Abstractions.Data.Interfaces;
using Mithril.Data.Inflatable.Databases;

namespace Mithril.Data.Inflatable.Mappings.General
{
    /// <summary>
    /// ILookUp mapping
    /// </summary>
    /// <seealso cref="MappingBaseClass{ILookUp, DefaultDatabase}"/>
    public class ILookUpMapping : MappingBaseClass<ILookUp, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ILookUpMapping"/> class.
        /// </summary>
        public ILookUpMapping()
            : base(merge: true)
        {
            Reference(x => x.DisplayName).WithDefaultValue(() => "");
            Reference(x => x.Icon).WithDefaultValue(() => "fa-info-circle");
            ManyToOne(x => x.Type);
        }
    }
}