using Mithril.API.Abstractions.Query.Interfaces;
using Mithril.API.Abstractions.Services;
using System.Security.Claims;

namespace Mithril.API.GraphQL.Services
{
    /// <summary>
    /// Drop down query service
    /// </summary>
    /// <seealso cref="IDropDownQueryService"/>
    public class DropDownQueryService : IDropDownQueryService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DropDownQueryService"/> class.
        /// </summary>
        /// <param name="dropDownQueries">The drop down queries.</param>
        public DropDownQueryService(IEnumerable<IDropDownQuery> dropDownQueries)
        {
            DropDownQueries = dropDownQueries.ToDictionary(x => x.Name);
        }

        /// <summary>
        /// Gets the drop down queries.
        /// </summary>
        /// <value>The drop down queries.</value>
        private Dictionary<string, IDropDownQuery> DropDownQueries { get; }

        /// <summary>
        /// Finds the drop down query.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="user">The user.</param>
        /// <returns>The drop down query specified.</returns>
        public IDropDownQuery? FindDropDownQuery(string? name, ClaimsPrincipal? user)
        {
            _ = DropDownQueries.TryGetValue(name ?? "", out IDropDownQuery? query);
            return query?.CanRun(name, user) == true ? query : null;
        }
    }
}