using GraphQL.Types;
using GraphQLParser.AST;
using System.Text.Json;

namespace Mithril.API.GraphQL.GraphTypes
{
    /// <summary>
    /// The Json scalar graph type represents a JSON document. (Copy of what should be built in to
    /// the library and will be taken out once merged into main on GraphQL.Net)
    /// </summary>
    public class JsonGraphType : ScalarGraphType
    {
        /// <summary>
        /// Checks for literal input coercion possibility. It takes an abstract syntax tree (AST)
        /// element from a schema definition or query and checks if it can be converted into an
        /// appropriate internal value. In other words it checks if a scalar can be converted from
        /// its client-side representation as an argument to its server-side representation.
        /// <br/><br/> This method can be overridden to validate input values without directly
        /// getting those values, i.e. without boxing. <br/><br/> This method must return <see
        /// langword="true"/> when passed a <see cref="T:GraphQLParser.AST.GraphQLNullValue"/> node.
        /// </summary>
        /// <param name="value">
        /// AST value node. Must not be <see langword="null"/>, but may be <see cref="T:GraphQLParser.AST.GraphQLNullValue"/>.
        /// </param>
        /// <returns></returns>
        public override bool CanParseLiteral(GraphQLValue value) => value is GraphQLStringValue || value is GraphQLNullValue;

        /// <summary>
        /// Checks for value input coercion possibility. Argument values can not only provided via
        /// GraphQL syntax inside a query, but also via variable. It checks if a scalar can be
        /// converted from its client-side representation as a variable to its server-side
        /// representation. <br/><br/> Parsing for arguments and variables are handled separately
        /// because while arguments must always be expressed in GraphQL query syntax, variable
        /// format is transport-specific (usually JSON). <br/><br/> This method can be overridden to
        /// validate input values without directly getting those values, i.e. without boxing.
        /// <br/><br/> This method must return <see langword="true"/> when passed a <see
        /// langword="null"/> value.
        /// </summary>
        /// <param name="value">Runtime object from variables. May be <see langword="null"/>.</param>
        /// <returns></returns>
        public override bool CanParseValue(object? value) => value is JsonDocument || value is string || value == null || value is IDictionary<string, object?>;

        /// <summary>
        /// Literal input coercion. It takes an abstract syntax tree (AST) element from a schema
        /// definition or query and converts it into an appropriate internal value. In other words
        /// it transforms a scalar from its client-side representation as an argument to its
        /// server-side representation. Input coercion may not only return primitive values like
        /// String but rather complex ones when appropriate. <br/><br/> This method must handle a
        /// value of <see cref="T:GraphQLParser.AST.GraphQLNullValue"/>. <br/><br/> This method
        /// SHOULD be overridden by descendants.
        /// </summary>
        /// <param name="value">
        /// AST value node. Must not be <see langword="null"/>, but may be <see cref="T:GraphQLParser.AST.GraphQLNullValue"/>.
        /// </param>
        /// <returns>Internal scalar representation. Returning <see langword="null"/> is valid.</returns>
        public override object? ParseLiteral(GraphQLValue value) => value switch
        {
            GraphQLStringValue s => JsonDocument.Parse(s.Value),
            GraphQLNullValue _ => null,
            _ => ThrowLiteralConversionError(value)
        };

        /// <summary>
        /// Value input coercion. Argument values can not only provided via GraphQL syntax inside a
        /// query, but also via variable. It transforms a scalar from its client-side representation
        /// as a variable to its server-side representation. <br/><br/> Parsing for arguments and
        /// variables are handled separately because while arguments must always be expressed in
        /// GraphQL query syntax, variable format is transport-specific (usually JSON). <br/><br/>
        /// This method must handle a value of <see langword="null"/>.
        /// </summary>
        /// <param name="value">Runtime object from variables. May be <see langword="null"/>.</param>
        /// <returns>Internal scalar representation. Returning <see langword="null"/> is valid.</returns>
        public override object? ParseValue(object? value) => value switch
        {
            JsonDocument _ => value,
            string s => JsonDocument.Parse(s),
            IDictionary<string, object?> e => JsonDocument.Parse(JsonSerializer.Serialize(e)),
            null => null,
            _ => ThrowValueConversionError(value)
        };
    }
}