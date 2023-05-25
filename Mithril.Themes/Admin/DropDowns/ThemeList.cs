using Mithril.API.Abstractions.Query.BaseClasses;
using Mithril.Themes.Models;

namespace Mithril.Themes.Admin.DropDowns
{
    /// <summary>
    /// Theme list
    /// TODO: Add tests
    /// </summary>
    /// <seealso cref="DropDownBaseClass&lt;Theme&gt;" />
    public class ThemeList : DropDownBaseClass<Theme>
    {
        /// <summary>
        /// Filters the query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The filtered query
        /// </returns>
        protected override IQueryable<Theme> FilterQuery(IQueryable<Theme> query, string value)
        {
            return query.Where(x => x.Name.StartsWith(value));
        }
    }
}