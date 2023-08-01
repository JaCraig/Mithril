using Mithril.API.Abstractions.Query.Interfaces;
using Mithril.API.Abstractions.Services;

namespace Mithril.API.GraphQL.Services
{
    /// <summary>
    /// Query service
    /// </summary>
    /// <seealso cref="IQueryService"/>
    public class QueryService : IQueryService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryService"/> class.
        /// </summary>
        /// <param name="queries">The queries.</param>
        public QueryService(IEnumerable<IQuery> queries)
        {
            Queries = queries.ToDictionary(x => x.Name);
        }

        /// <summary>
        /// Gets the queries.
        /// </summary>
        /// <value>The queries.</value>
        private Dictionary<string, IQuery> Queries { get; }

        /// <summary>
        /// Finds the query specified.
        /// </summary>
        /// <param name="name">The name of the query.</param>
        /// <returns>The query specified.</returns>
        public IQuery? FindQuery(string? name)
        {
            Queries.TryGetValue(name ?? "", out IQuery? query);
            return query;
        }
    }
}