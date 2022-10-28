using Inflatable.BaseClasses;
using Mithril.Data.Abstractions.Interfaces;
using Mithril.Data.Inflatable.Databases;

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
            Reference(x => x.Type).WithMaxLength(128);
            ManyToMany(x => x.Users).OnDeleteDoNothing();
            Reference(x => x.Value).WithMaxLength();
        }
    }
}