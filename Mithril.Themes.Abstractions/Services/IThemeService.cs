using Mithril.Themes.Abstractions.Interfaces;
using System.Security.Claims;

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
        /// <returns>The themes available.</returns>
        IEnumerable<ITheme> GetAvailableThemes();

        /// <summary>
        /// Loads the theme based on the name.
        /// </summary>
        /// <param name="themeName">Name of the theme.</param>
        /// <returns>The theme associated with the name.</returns>
        ITheme? LoadTheme(string? themeName = "Default");

        /// <summary>
        /// Sets the default theme.
        /// </summary>
        /// <param name="theme">The theme.</param>
        /// <param name="user">The user.</param>
        /// <returns>This.</returns>
        Task<IThemeService> SetDefaultThemeAsync(ITheme? theme, ClaimsPrincipal? user);

        /// <summary>
        /// Sets an alias for a theme
        /// </summary>
        /// <param name="themeName">The alias name</param>
        /// <param name="theme">The theme</param>
        /// <returns>This.</returns>
        IThemeService SetThemeAlias(string? themeName, ITheme? theme);
    }
}