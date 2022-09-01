using Inflatable.BaseClasses;
using Mithril.Data.Inflatable.Databases;
using Mithril.Data.Models.Security;

namespace Mithril.Data.Mappings.Security
{
    /// <summary>
    /// User mapping
    /// </summary>
    /// <seealso cref="MappingBaseClass{User, DefaultDatabase}"/>
    public class UserMapping : MappingBaseClass<User, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserMapping"/> class.
        /// </summary>
        public UserMapping()
        {
            ManyToMany(x => x.Claims);
            ManyToOne(x => x.ContactInformation).CascadeChanges();
            Reference(x => x.FirstName).WithMaxLength(100);
            Reference(x => x.LastName).WithMaxLength(100);
            Reference(x => x.Title).WithMaxLength(100);
            Reference(x => x.MiddleName).WithMaxLength(100);
            Reference(x => x.NickName).WithMaxLength(100);
            Reference(x => x.Prefix).WithMaxLength(30);
            Reference(x => x.Suffix).WithMaxLength(30);
        }
    }
}