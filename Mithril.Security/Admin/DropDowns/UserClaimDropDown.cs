using Mithril.API.Abstractions.Query.BaseClasses;
using Mithril.Security.Models;

namespace Mithril.Security.Admin.DropDowns
{
    /// <summary>
    /// UserClaim drop down
    /// </summary>
    /// <seealso cref="DropDownBaseClass&lt;UserClaim&gt;" />
    public class UserClaimDropDown : DropDownBaseClass<UserClaim>
    {
        /// <summary>
        /// Filters the query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="value">The value.</param>
        /// <returns>The filtered query</returns>
        protected override IQueryable<UserClaim> FilterQuery(IQueryable<UserClaim> query, string value) => query.Where(x => x.Value.StartsWith(value));
    }
}