using Inflatable.BaseClasses;
using Mithril.Data.Abstractions.Databases;
using Mithril.Security.Abstractions.Interfaces;

namespace Mithril.Security.Models.Mappings
{
    /// <summary>
    /// ITenant mapping
    /// </summary>
    /// <seealso cref="MappingBaseClass{ITenant, DefaultDatabase}"/>
    public class ITenantMapping : MappingBaseClass<ITenant, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ITenantMapping"/> class.
        /// </summary>
        public ITenantMapping()
            : base(merge: true)
        {
            _ = Reference(x => x.DisplayName).WithMaxLength(100);
            _ = ManyToOne(x => x.Users).CascadeChanges();
        }
    }
}