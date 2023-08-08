using Mithril.Themes.Abstractions.Interfaces;

namespace Mithril.Themes.Abstractions.BaseClasses
{
    /// <summary>
    /// Theme base class
    /// </summary>
    /// <typeparam name="TTheme">The type of the theme.</typeparam>
    /// <seealso cref="ITheme"/>
    public abstract class ThemeBaseClass<TTheme> : ITheme
        where TTheme : ThemeBaseClass<TTheme>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThemeBaseClass{TTheme}"/> class.
        /// </summary>
        protected ThemeBaseClass()
        { }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; } = typeof(TTheme).Name.Replace("Theme", "");

        /// <summary>
        /// Gets the zones.
        /// </summary>
        /// <value>The zones.</value>
        public IEnumerable<string> Zones { get; } = Array.Empty<string>();
    }
}