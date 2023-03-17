using Inflatable.BaseClasses;
using Mithril.Data.Abstractions.Databases;

namespace Mithril.Themes.Models.Mappings
{
    /// <summary>
    /// Theme change event mapping
    /// </summary>
    public class ThemeChangedEventMapping : MappingBaseClass<ThemeChangedEvent, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThemeChangedEventMapping"/> class.
        /// </summary>
        public ThemeChangedEventMapping()
        {
            Reference(x => x.ThemeName);
        }
    }
}