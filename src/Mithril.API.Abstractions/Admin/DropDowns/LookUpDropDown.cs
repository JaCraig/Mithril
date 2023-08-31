using Mithril.API.Abstractions.Query.BaseClasses;
using Mithril.Data.Abstractions.Interfaces;

namespace Mithril.API.Abstractions.Admin.DropDowns
{
    /// <summary>
    /// LookUp drop down
    /// </summary>
    /// <seealso cref="DropDownBaseClass&lt;ILookUp&gt;" />
    public class LookUpDropDown : DropDownBaseClass<ILookUp>
    {
        /// <summary>
        /// Filters the query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="value">The value.</param>
        /// <returns>The filtered query</returns>
        protected override IQueryable<ILookUp> FilterQuery(IQueryable<ILookUp> query, string value) => query.Where(x => x.DisplayName.StartsWith(value));
    }
}