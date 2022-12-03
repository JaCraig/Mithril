using GraphQL.Types;
using Mithril.API.Abstractions.Query.Interfaces;

namespace Mithril.API.GraphQL.GraphTypes.ExtensionMethods
{
    /// <summary>
    /// IArgument extensions
    /// </summary>
    public static class IArgumentExtensions
    {
        /// <summary>
        /// Creates the argument object.
        /// </summary>
        /// <param name="argument">The x.</param>
        /// <returns>The query argument.</returns>
        public static QueryArgument? CreateArgument(this IArgument argument)
        {
            if (argument is null)
                return null;
            var GraphType = argument.ArgumentType.FindGraphType();
            if (GraphType is null)
                return null;
            return new QueryArgument(GraphType) { Name = argument.Name ?? "", Description = argument.Description, DefaultValue = argument.DefaultValue };
        }
    }
}