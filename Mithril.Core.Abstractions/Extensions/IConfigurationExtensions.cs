using Microsoft.Extensions.Configuration;
using Mithril.Core.Abstractions.Configuration;

namespace Mithril.Core.Abstractions.Extensions
{
    /// <summary>
    /// Configuration extensions
    /// </summary>
    public static class IConfigurationExtensions
    {
        /// <summary>
        /// Gets the system configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The system configuration.</returns>
        public static MithrilConfig? GetSystemConfig(this IConfiguration? configuration) => configuration?.GetSection("Mithril").Get<MithrilConfig>();
    }
}