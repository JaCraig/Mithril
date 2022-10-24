using BigBook;
using GraphQL.Types;
using Mithril.API.GraphQL.Authorization;
using Mithril.API.GraphQL.GraphTypes;
using Mithril.API.Query.Interfaces;
using System.Reflection;

namespace Mithril.API.GraphQL.ObjectGraphs
{
    /// <summary>
    /// Composite query that creates the root node.
    /// </summary>
    /// <seealso cref="GraphQL.Types.ObjectGraphType"/>
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
            var GenericAddField = Methods.FirstOrDefault(x => x.IsGenericMethod && x.Name == nameof(AddField));
            foreach (var Query in queries)
            {
                var Temp = GenericAddField?.MakeGenericMethod(Query.ReturnType);
                Temp?.Invoke(this, new object?[] { Query });
            }
        }

        /// <summary>
        /// Adds the field.
        /// </summary>
        /// <typeparam name="TReturnType">The type of the return type.</typeparam>
        /// <param name="query">The query.</param>
        private void AddField<TReturnType>(IQuery<TReturnType> query)
            where TReturnType : class
        {
            Field<TReturnType>(query.Name, query.Nullable ?? false)
                    .Description(query.Description)
                    .Arguments(query.Arguments.ToArray(x => new QueryArgument<LongGraphType> { Name = x.Name, Description = x.Description, DefaultValue = x.DefaultValue }))
                    .Type(new GenericGraphType<TReturnType>())
                    .ResolveAsync(context =>
                    {
                        var CurrentUser = (context.UserContext as GraphQLUserContextDictionary)?.User;
                        return query.Resolver.ResolveAsync(CurrentUser, new Query.Arguments());
                    })
                    .DeprecationReason(string.IsNullOrEmpty(query.DeprecationReason) ? null : query.DeprecationReason);
        }
    }
}