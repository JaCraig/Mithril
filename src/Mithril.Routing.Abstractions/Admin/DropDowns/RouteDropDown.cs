using Mithril.API.Abstractions.Query.BaseClasses;
using Mithril.Routing.Abstractions.Interfaces;

namespace Mithril.Routing.Abstractions.Admin.DropDowns
{
    /// <summary>
    /// Route drop down
    /// </summary>
    /// <seealso cref="DropDownBaseClass&lt;IRoute&gt;" />
    public class RouteDropDown : DropDownBaseClass<IRoute>
    {
        /// <summary>
        /// Filters the query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="value">The value.</param>
        /// <returns>The filtered query</returns>
        protected override IQueryable<IRoute> FilterQuery(IQueryable<IRoute> query, string value)
            => query.Where(x => x.InputPath.StartsWith(value)
                            || x.OutputPath.StartsWith(value));
    }
}