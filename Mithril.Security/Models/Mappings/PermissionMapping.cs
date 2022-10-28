using Inflatable.BaseClasses;
using Mithril.Data.Inflatable.Databases;

namespace Mithril.Security.Models.Mappings
{
    /// <summary>
    /// Permission mapping
    /// </summary>
    /// <seealso cref="MappingBaseClass{Permission, DefaultDatabase}"/>
    public class PermissionMapping : MappingBaseClass<Permission, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionMapping"/> class.
        /// </summary>
        public PermissionMapping()
        {
            ManyToMany(x => x.Claims);
        }
    }
}