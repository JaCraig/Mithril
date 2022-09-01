using Inflatable.BaseClasses;
using Mithril.Core.Abstractions.Security.Interfaces;
using Mithril.Data.Inflatable.Databases;

namespace Mithril.Data.Mappings.Security
{
    /// <summary>
    /// IUserClaim mapping
    /// </summary>
    /// <seealso cref="MappingBaseClass{IUserClaim, DefaultDatabase}"/>
    public class IUserClaimMapping : MappingBaseClass<IUserClaim, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IUserClaimMapping"/> class.
        /// </summary>
        public IUserClaimMapping()
            : base(merge: true)
        {
            Reference(x => x.Type).WithMaxLength(128);
            ManyToMany(x => x.Users);
            Reference(x => x.Value).WithMaxLength();
        }
    }
}