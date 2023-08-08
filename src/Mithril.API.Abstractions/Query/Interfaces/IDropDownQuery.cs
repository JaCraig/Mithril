using Mithril.API.Abstractions.Query.ViewModels;
using System.Security.Claims;

namespace Mithril.API.Abstractions.Query.Interfaces
{
    /// <summary>
    /// Drop down query interface
    /// </summary>
    public interface IDropDownQuery
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        /// Determines whether this instance can run the specified data type.
        /// </summary>
        /// <param name="dataType">Type of the data.</param>
        /// <param name="user">The user.</param>
        /// <returns>
        /// <c>true</c> if this instance can run the specified data type; otherwise, <c>false</c>.
        /// </returns>
        bool CanRun(string? dataType, ClaimsPrincipal? user);

        /// <summary>
        /// Gets the data asynchronously.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The drop down items.</returns>
        Task<IEnumerable<DropDownVM<long>>> GetDataAsync(string? filter);
    }
}