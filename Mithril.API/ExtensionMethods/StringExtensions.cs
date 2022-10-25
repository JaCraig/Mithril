namespace Mithril.API.ExtensionMethods
{
    /// <summary>
    /// String extensions
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Splits the camel case.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static string SplitCamelCase(this string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "";
            return System.Text.RegularExpressions.Regex.Replace(input, "([A-Z])", " $1", System.Text.RegularExpressions.RegexOptions.Compiled).Trim();
        }
    }
}