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
        /// Gets the configuration section requested.
        /// </summary>
        /// <typeparam name="TOptions">The type of the options.</typeparam>
        /// <param name="configuration">The configuration.</param>
        /// <param name="section">The section.</param>
        /// <returns>The config specified.</returns>
        public static TOptions? GetConfig<TOptions>(this IConfiguration? configuration, string section)
            where TOptions : class => configuration?.GetSection(section).Get<TOptions>();

        /// <summary>
        /// Gets the system configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The system configuration.</returns>
        public static MithrilConfig? GetSystemConfig(this IConfiguration? configuration) => configuration?.GetConfig<MithrilConfig>("Mithril");
    }
}