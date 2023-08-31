using Mithril.API.Abstractions.Query.BaseClasses;
using Mithril.Data.Abstractions.Interfaces;

namespace Mithril.API.Abstractions.Admin.DropDowns
{
    /// <summary>
    /// UserClaim drop down
    /// </summary>
    /// <seealso cref="DropDownBaseClass&lt;UserClaim&gt;" />
    public class UserClaimDropDown : DropDownBaseClass<IUserClaim>
    {
        /// <summary>
        /// Filters the query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="value">The value.</param>
        /// <returns>The filtered query</returns>
        protected override IQueryable<IUserClaim> FilterQuery(IQueryable<IUserClaim> query, string value) => query.Where(x => x.Value.StartsWith(value));
    }
}