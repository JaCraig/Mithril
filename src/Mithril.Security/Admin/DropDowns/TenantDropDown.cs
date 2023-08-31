using Mithril.API.Abstractions.Query.BaseClasses;
using Mithril.Security.Models;

namespace Mithril.Security.Admin.DropDowns
{
    /// <summary>
    /// Tenant drop down
    /// </summary>
    /// <seealso cref="DropDownBaseClass&lt;Tenant&gt;" />
    public class TenantDropDown : DropDownBaseClass<Tenant>
    {
        /// <summary>
        /// Filters the query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The filtered query
        /// </returns>
        protected override IQueryable<Tenant> FilterQuery(IQueryable<Tenant> query, string value)
        {
            return query.Where(x => x.DisplayName.StartsWith(value));
        }
    }
}