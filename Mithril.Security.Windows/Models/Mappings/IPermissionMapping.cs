using Inflatable.BaseClasses;
using Mithril.Core.Abstractions.Security.Interfaces;
using Mithril.Data.Inflatable.Databases;

namespace Mithril.Security.Windows.Models.Mappings
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