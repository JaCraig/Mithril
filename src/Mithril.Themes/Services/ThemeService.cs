using BigBook;
using Mithril.Data.Abstractions.Services;
using Mithril.Themes.Abstractions.Interfaces;
using Mithril.Themes.Abstractions.Services;
using Mithril.Themes.Models;
using System.Security.Claims;

namespace Mithril.Themes.Services
{
    /// <summary>
    /// Theme manager
    /// </summary>
    /// <seealso cref="IThemeService"/>
    public class ThemeService : IThemeService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThemeService"/> class.
        /// </summary>
        /// <param name="themes">The themes.</param>
        /// <param name="dataService">The data service.</param>
        public ThemeService(IEnumerable<ITheme> themes, IDataService? dataService)
        {
            DataService = dataService;
            Themes = themes.ToDictionary(x => x.Name);
            Theme? DefaultTheme = Theme.Query(dataService)?.Where(x => x.IsDefault && x.Active).FirstOrDefault();
            if (!Themes.TryGetValue(DefaultTheme?.Name ?? "Default", out ITheme? TempTheme))
                TempTheme = themes.FirstOrDefault();
            _ = AsyncHelper.RunSync(() => SetDefaultThemeAsync(TempTheme, null));
        }

        /// <summary>
        /// Gets the data service.
        /// </summary>
        /// <value>The data service.</value>
        private IDataService? DataService { get; }

        /// <summary>
        /// Gets the themes.
        /// </summary>
        /// <value>The themes.</value>
        private IDictionary<string, ITheme> Themes { get; }

        /// <summary>
        /// Gets the themes.
        /// </summary>
        /// <returns>The themes available.</returns>
        public IEnumerable<ITheme> GetAvailableThemes() => Themes.Values;

        /// <summary>
        /// Loads the theme based on the name.
        /// </summary>
        /// <param name="themeName">Name of the theme.</param>
        /// <returns>The theme associated with the name.</returns>
        public ITheme? LoadTheme(string? themeName = "Default")
        {
            _ = Themes.TryGetValue(themeName ?? "", out ITheme? theme);
            return theme;
        }

        /// <summary>
        /// Sets the default theme.
        /// </summary>
        /// <param name="theme">The theme.</param>
        /// <param name="user">The user.</param>
        /// <returns>This.</returns>
        public async Task<IThemeService> SetDefaultThemeAsync(ITheme? theme, ClaimsPrincipal? user)
        {
            if (theme is null)
                return this;
            _ = SetThemeAlias("Default", theme);
            var Command = new SetThemeCommand(theme.Name);
            await Command.SaveAsync(DataService, user).ConfigureAwait(false);
            return this;
        }

        /// <summary>
        /// Set an alias for a theme
        /// </summary>
        /// <param name="themeName">Theme alias name</param>
        /// <param name="theme">Theme</param>
        /// <returns>This.</returns>
        public IThemeService SetThemeAlias(string? themeName, ITheme? theme)
        {
            if (string.IsNullOrEmpty(themeName) || theme is null)
                return this;
            Themes[themeName] = theme;
            return this;
        }
    }
}