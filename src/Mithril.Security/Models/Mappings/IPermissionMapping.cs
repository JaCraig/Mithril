using Inflatable.BaseClasses;
using Mithril.Data.Abstractions.Databases;
using Mithril.Security.Abstractions.Interfaces;

namespace Mithril.Security.Models.Mappings
{
    /// <summary>
    /// Permission mapping
    /// </summary>
    /// <seealso cref="MappingBaseClass{IPermission, DefaultDatabase}"/>
    public class IPermissionMapping : MappingBaseClass<IPermission, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IPermissionMapping"/> class.
        /// </summary>
        public IPermissionMapping()
            : base(merge: true)
        {
            Reference(x => x.DisplayName).WithMaxLength(128);
            Reference(x => x.Operand);
        }
    }
}