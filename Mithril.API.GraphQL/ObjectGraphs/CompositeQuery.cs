using GraphQL.Types;
using Mithril.API.Query.Interfaces;

namespace Mithril.API.GraphQL.ObjectGraphs
{
    public class CompositeQuery : ObjectGraphType
    {
        public CompositeQuery(IEnumerable<IQuery> queries)
        {
            queries ??= Array.Empty<IQuery>();
            Name = "RootQuery";
            Description = "Root query";
            foreach (var Query in queries)
            {
            }
        }
    }
}