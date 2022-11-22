using BigBook;
using GraphQL.Types;
using Mithril.API.Abstractions.Query;
using Mithril.API.Abstractions.Query.Interfaces;
using Mithril.API.GraphQL.Authorization;
using Mithril.API.GraphQL.ExtensionMethods;
using Mithril.API.GraphQL.GraphTypes;
using System.Reflection;

namespace Mithril.API.GraphQL.ObjectGraphs
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
                var Temp = GenericAddField?.MakeGenericMethod(Query.ReturnType);
                Temp?.Invoke(this, new object?[] { Query });
            }
        }

        /// <summary>
        /// Creates the argument object.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="x">The x.</param>
        /// <returns>The query argument.</returns>
        private static QueryArgument? CreateArgument<TValue>(IArgument x)
            where TValue : IGraphType
        {
            if (x is null)
                return null;
            return new QueryArgument<TValue> { Name = x.Name, Description = x.Description, DefaultValue = x.DefaultValue };
        }

        /// <summary>
        /// Adds the field.
        /// </summary>
        /// <typeparam name="TReturnType">The type of the return type.</typeparam>
        /// <param name="query">The query.</param>
        private void AddField<TReturnType>(IQuery<TReturnType> query)
            where TReturnType : class
        {
            var Methods = typeof(CompositeQuery).GetMethods(BindingFlags.NonPublic | BindingFlags.Static);
            var GenericCreateArgument = Array.Find(Methods, x => x.IsGenericMethod && x.Name == nameof(CreateArgument));
            Field<TReturnType>(query.Name, query.Nullable ?? false)
                    .Description(query.Description)
                    .Arguments(query.Arguments?.ToArray(x => (QueryArgument?)GenericCreateArgument?.MakeGenericMethod(x.ArgumentType.FindGraphType())?.Invoke(this, new object?[] { x })))
                    .Type(new GenericGraphType<TReturnType>())
                    .ResolveAsync(context =>
                    {
                        var CurrentUser = (context.UserContext as GraphQLUserContextDictionary)?.User;
                        var Arguments = new Arguments();
                        context.Arguments.ForEach(x => Arguments.Add(x.Key, x.Value.Value));
                        return query.ResolveAsync(CurrentUser, Arguments);
                    })
                    .DeprecationReason(string.IsNullOrEmpty(query.DeprecationReason) ? null : query.DeprecationReason);
        }
    }
}