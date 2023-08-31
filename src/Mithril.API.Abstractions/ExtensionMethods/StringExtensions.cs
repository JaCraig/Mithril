using System.Text.RegularExpressions;

namespace Mithril.API.Abstractions.ExtensionMethods
{
    /// <summary>
    /// String extensions
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// The camel case regex
        /// </summary>
        private static readonly Regex _CamelCaseRegex = new("([A-Z])", RegexOptions.Compiled);

        /// <summary>
        /// Splits the camel case.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The split camel case string</returns>
        public static string SplitCamelCase(this string? input) => string.IsNullOrWhiteSpace(input) ? "" : _CamelCaseRegex.Replace(input, " $1").Trim();
    }
}