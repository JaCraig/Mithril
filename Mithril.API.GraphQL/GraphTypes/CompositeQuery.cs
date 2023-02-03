using BigBook;
using GraphQL.Types;
using Mithril.API.Abstractions.Query;
using Mithril.API.Abstractions.Query.Interfaces;
using Mithril.API.GraphQL.Authorization;
using Mithril.API.GraphQL.GraphTypes.ExtensionMethods;
using System.Reflection;

namespace Mithril.API.GraphQL.GraphTypes
{
    /// <summary>
    /// Composite query that creates the root node.
    /// </summary>
    /// <seealso cref="ObjectGraphType"/>
    public class CompositeQuery : ObjectGraphType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeQuery"/> class.
        /// </summary>
        /// <param name="queries">The queries.</param>
        public CompositeQuery(IEnumerable<IQuery> queries)
        {
            queries ??= Array.Empty<IQuery>();
            Name = "RootQuery";
            Description = "Root query";
            var Methods = typeof(CompositeQuery).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
            var GenericAddField = Array.Find(Methods, x => x.IsGenericMethod && x.Name == nameof(AddField));
            foreach (var Query in queries)
            {
                var GraphType = Query.ReturnType.FindGraphType();
                if (GraphType is null)
                    continue;
                GenericAddField?.MakeGenericMethod(Query.ReturnType, GraphType).Invoke(this, new object[] { Query });
            }
        }

        /// <summary>
        /// Adds the field.
        /// </summary>
        /// <typeparam name="TReturnType">The type of the return type.</typeparam>
        /// <typeparam name="TGraphType">The type of the graph type.</typeparam>
        /// <param name="query">The query.</param>
        [Obsolete]
        private void AddField<TReturnType, TGraphType>(IQuery<TReturnType> query)
            where TReturnType : class
            where TGraphType : class, IGraphType
        {
            Field<TGraphType>(name: query.Name,
                    description: query.Description,
                    arguments: new QueryArguments(query.Arguments?.ToArray(x => x.CreateArgument()!) ?? Array.Empty<QueryArgument>()),
                    resolve: context =>
                    {
                        var CurrentUser = (context.UserContext as GraphQLUserContextDictionary)?.User;
                        var Arguments = new Arguments();
                        context.Arguments.ForEach(x => Arguments.Add(x.Key, x.Value.Value));
                        return AsyncHelper.RunSync(() => query.ResolveAsync(CurrentUser, Arguments));
                    },
                    deprecationReason: string.IsNullOrEmpty(query.DeprecationReason) ? null : query.DeprecationReason)
                .SetSecurity(query.GetType());
        }
    }
}