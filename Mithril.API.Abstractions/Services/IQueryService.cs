using Mithril.API.Abstractions.Query.Interfaces;

namespace Mithril.API.Abstractions.Services
{
    /// <summary>
    /// Query service
    /// </summary>
    public interface IQueryService
    {
        /// <summary>
        /// Finds the drop down query.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        /// The drop down query specified.
        /// </returns>
        public IDropDownQuery? FindDropDownQuery(string name);

        /// <summary>
        /// Finds the query specified.
        /// </summary>
        /// <param name="name">The name of the query.</param>
        /// <returns>The query specified.</returns>
        public IQuery? FindQuery(string name);
    }
}