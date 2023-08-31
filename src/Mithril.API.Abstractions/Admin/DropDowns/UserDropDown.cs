using Mithril.API.Abstractions.Query.BaseClasses;
using Mithril.Data.Abstractions.Interfaces;

namespace Mithril.API.Abstractions.Admin.DropDowns
{
    /// <summary>
    /// User drop down
    /// </summary>
    /// <seealso cref="DropDownBaseClass&lt;IUser&gt;" />
    public class UserDropDown : DropDownBaseClass<IUser>
    {
        /// <summary>
        /// Filters the query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="value">The value.</param>
        /// <returns>The filtered query</returns>
        protected override IQueryable<IUser> FilterQuery(IQueryable<IUser> query, string value) => query.Where(x => x.UserName.StartsWith(value));
    }
}