namespace Mithril.Themes.Abstractions.Interfaces
{
    /// <summary>
    /// Theme interface
    /// </summary>
    public interface ITheme
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        /// Gets the zones.
        /// </summary>
        /// <value>The zones.</value>
        IEnumerable<string> Zones { get; }
    }
}