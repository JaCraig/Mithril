using Inflatable.BaseClasses;
using Mithril.Data.Abstractions.Databases;

namespace Mithril.Security.Models.Mappings
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
            _ = ManyToMany(x => x.Claims);
            _ = ManyToOne(x => x.ContactInformation).CascadeChanges();
            _ = Reference(x => x.FirstName).WithMaxLength(100);
            _ = Reference(x => x.LastName).WithMaxLength(100);
            _ = Reference(x => x.Title).WithMaxLength(100);
            _ = Reference(x => x.MiddleName).WithMaxLength(100);
            _ = Reference(x => x.NickName).WithMaxLength(100);
            _ = Reference(x => x.Prefix).WithMaxLength(30);
            _ = Reference(x => x.Suffix).WithMaxLength(30);
        }
    }
}