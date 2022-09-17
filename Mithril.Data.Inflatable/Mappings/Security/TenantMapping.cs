using Inflatable.BaseClasses;
using Mithril.Data.Inflatable.Databases;
using Mithril.Data.Models.Security;

namespace Mithril.Data.Mappings.Security
{
    /// <summary>
    /// Tenant mapping
    /// </summary>
    /// <seealso cref="MappingBaseClass{Tenant, DefaultDatabase}"/>
    public class TenantMapping : MappingBaseClass<Tenant, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TenantMapping"/> class.
        /// </summary>
        public TenantMapping()
        {
        }
    }
}