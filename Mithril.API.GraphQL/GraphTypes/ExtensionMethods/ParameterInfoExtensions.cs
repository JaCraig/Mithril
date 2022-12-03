using BigBook;
using GraphQL;
using GraphQL.Types;
using System.Reflection;

namespace Mithril.API.GraphQL.GraphTypes.ExtensionMethods
{
    /// <summary>
    /// ParameterInfo extensions
    /// </summary>
    public static class ParameterInfoExtensions
    {
        /// <summary>
        /// Creates the query argument object.
        /// </summary>
        /// <param name="parameterInfo">The parameter information.</param>
        /// <returns>The query argument.</returns>
        public static QueryArgument? ToQueryArgument(this ParameterInfo parameterInfo)
        {
            if (parameterInfo is null) return null;

            var GraphType = parameterInfo.ParameterType.FindGraphType();
            if (GraphType is null) return null;

            return new QueryArgument(GraphType)
            {
                Name = (parameterInfo.Name ?? "").ToCamelCase(),
                Description = parameterInfo.Name.AddSpaces().ToString(StringCase.SentenceCapitalize),
                DefaultValue = parameterInfo.HasDefaultValue ? parameterInfo.DefaultValue : null
            };
        }
    }
}