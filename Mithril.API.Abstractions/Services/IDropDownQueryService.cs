using Mithril.API.Abstractions.Query.Interfaces;
using System.Security.Claims;

namespace Mithril.API.Abstractions.Services
{
    /// <summary>
    /// Drop down query service
    /// </summary>
    public interface IDropDownQueryService
    {
        /// <summary>
        /// Finds the drop down query.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="user">The user.</param>
        /// <returns>The drop down query specified.</returns>
        public IDropDownQuery? FindDropDownQuery(string? name, ClaimsPrincipal? user);
    }
}