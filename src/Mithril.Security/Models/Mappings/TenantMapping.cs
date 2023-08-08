using Inflatable.BaseClasses;
using Mithril.Data.Abstractions.Databases;

namespace Mithril.Security.Models.Mappings
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