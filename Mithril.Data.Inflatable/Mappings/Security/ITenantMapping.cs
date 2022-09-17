using Inflatable.BaseClasses;
using Mithril.Core.Abstractions.Security.Interfaces;
using Mithril.Data.Inflatable.Databases;

namespace Mithril.Data.Mappings.Security
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
            Reference(x => x.DisplayName).WithMaxLength(100);
        }
    }
}