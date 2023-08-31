using Mithril.API.Abstractions.Query.BaseClasses;
using Mithril.Data.Abstractions.Interfaces;

namespace Mithril.API.Abstractions.Admin.DropDowns
{
    /// <summary>
    /// LookUpType drop down
    /// </summary>
    /// <seealso cref="DropDownBaseClass&lt;ILookUpType&gt;" />
    public class LookUpTypeDropDown : DropDownBaseClass<ILookUpType>
    {
        /// <summary>
        /// Filters the query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="value">The value.</param>
        /// <returns>The filtered query</returns>
        protected override IQueryable<ILookUpType> FilterQuery(IQueryable<ILookUpType> query, string value) => query.Where(x => x.DisplayName.StartsWith(value));
    }
}