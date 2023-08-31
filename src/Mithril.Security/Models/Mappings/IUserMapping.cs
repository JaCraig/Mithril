using Inflatable.BaseClasses;
using Mithril.Data.Abstractions.Databases;
using Mithril.Data.Abstractions.Interfaces;

namespace Mithril.Security.Models.Mappings
{
    /// <summary>
    /// IUser mapping
    /// </summary>
    /// <seealso cref="MappingBaseClass{IUser, DefaultDatabase}"/>
    public class IUserMapping : MappingBaseClass<IUser, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IUserMapping"/> class.
        /// </summary>
        public IUserMapping()
            : base(merge: true)
        {
            _ = Reference(x => x.UserName).WithMaxLength(100).IsUnique();
        }
    }
}