using Mithril.Themes.Abstractions.Interfaces;
using Mithril.Themes.Abstractions.Services;

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
        public ThemeService(IEnumerable<ITheme> themes)
        {
            Themes = themes.ToDictionary(x => x.Name);
        }

        /// <summary>
        /// Gets the themes.
        /// </summary>
        /// <value>The themes.</value>
        public IDictionary<string, ITheme> Themes { get; }
    }
}