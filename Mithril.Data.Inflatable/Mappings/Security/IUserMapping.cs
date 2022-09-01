using Inflatable.BaseClasses;
using Mithril.Core.Abstractions.Security.Interfaces;
using Mithril.Data.Inflatable.Databases;

namespace Mithril.Data.Mappings.Security
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
            Reference(x => x.UserName).WithMaxLength(100).IsUnique();
        }
    }
}