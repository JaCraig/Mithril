using Inflatable.BaseClasses;
using Mithril.Data.Abstractions.Databases;
using Mithril.Data.Abstractions.Interfaces;

namespace Mithril.Security.Models.Mappings
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
            _ = Reference(x => x.Type).WithMaxLength(128);
            _ = ManyToMany(x => x.Users).OnDeleteDoNothing();
            _ = Reference(x => x.Value).WithMaxLength();
        }
    }
}