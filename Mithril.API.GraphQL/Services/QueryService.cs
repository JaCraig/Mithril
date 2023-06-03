using Mithril.API.Abstractions.Query.Interfaces;
using Mithril.API.Abstractions.Services;

namespace Mithril.API.GraphQL.Services
{
    /// <summary>
    /// Query service
    /// TODO: Add tests.
    /// </summary>
    /// <seealso cref="IQueryService" />
    public class QueryService : IQueryService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryService" /> class.
        /// </summary>
        /// <param name="queries">The queries.</param>
        /// <param name="dropDownQueries">The drop down queries.</param>
        public QueryService(IEnumerable<IQuery> queries, IEnumerable<IDropDownQuery> dropDownQueries)
        {
            Queries = queries.ToDictionary(x => x.Name);
            DropDownQueries = dropDownQueries.ToDictionary(x => x.Name);
        }

        /// <summary>
        /// Gets the drop down queries.
        /// </summary>
        /// <value>
        /// The drop down queries.
        /// </value>
        private Dictionary<string, IDropDownQuery> DropDownQueries { get; }

        /// <summary>
        /// Gets the queries.
        /// </summary>
        /// <value>
        /// The queries.
        /// </value>
        private Dictionary<string, IQuery> Queries { get; }

        /// <summary>
        /// Finds the drop down query.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        /// The drop down query specified.
        /// </returns>
        public IDropDownQuery? FindDropDownQuery(string name)
        {
            DropDownQueries.TryGetValue(name, out var query);
            return query;
        }

        /// <summary>
        /// Finds the query specified.
        /// </summary>
        /// <param name="name">The name of the query.</param>
        /// <returns>
        /// The query specified.
        /// </returns>
        public IQuery? FindQuery(string name)
        {
            Queries.TryGetValue(name, out var query);
            return query;
        }
    }
}