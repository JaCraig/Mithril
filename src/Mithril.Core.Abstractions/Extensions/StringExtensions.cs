using System.Globalization;

namespace Mithril.Core.Abstractions.Extensions
{
    /// <summary>
    /// String extension methods
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Converts the string to pascal case.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The value in pascal case.</returns>
        public static string ToPascalCase(this string? value)
        {
            if (string.IsNullOrEmpty(value))
                return "";
            return char.ToUpper(value[0], CultureInfo.InvariantCulture) + value.Remove(0, 1);
        }
    }
}