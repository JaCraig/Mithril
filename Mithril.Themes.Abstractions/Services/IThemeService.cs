using Mithril.Themes.Abstractions.Interfaces;

namespace Mithril.Themes.Abstractions.Services
{
    /// <summary>
    /// Theme manager interface
    /// </summary>
    public interface IThemeService
    {
        /// <summary>
        /// Gets the themes.
        /// </summary>
        /// <value>The themes.</value>
        IDictionary<string, ITheme> Themes { get; }
    }
}