using Mithril.API.Abstractions.Query.Interfaces;
using Mithril.API.Abstractions.Services;

namespace Mithril.API.GraphQL.Services
{
    /// <summary>
    /// Query service
    /// </summary>
    /// <seealso cref="IQueryService"/>
    /// <remarks>
    /// Initializes a new instance of the <see cref="QueryService"/> class.
    /// </remarks>
    /// <param name="queries">The queries.</param>
    public class QueryService(IEnumerable<IQuery> queries) : IQueryService
    {
        /// <summary>
        /// Gets the queries.
        /// </summary>
        /// <value>The queries.</value>
        private Dictionary<string, IQuery> Queries { get; } = queries.ToDictionary(x => x.Name);

        /// <summary>
        /// Finds the query specified.
        /// </summary>
        /// <param name="name">The name of the query.</param>
        /// <returns>The query specified.</returns>
        public IQuery? FindQuery(string? name)
        {
            _ = Queries.TryGetValue(name ?? "", out IQuery? query);
            return query;
        }
    }
}