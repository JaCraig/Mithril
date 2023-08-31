using Inflatable.BaseClasses;
using Mithril.Data.Abstractions.Databases;

namespace Mithril.Themes.Models.Mappings
{
    /// <summary>
    /// Set theme command mapping
    /// </summary>
    /// <seealso cref="MappingBaseClass&lt;SetThemeCommand, DefaultDatabase&gt;"/>
    public class SetThemeCommandMapping : MappingBaseClass<SetThemeCommand, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetThemeCommandMapping"/> class.
        /// </summary>
        public SetThemeCommandMapping()
        {
            _ = Reference(x => x.ThemeName);
        }
    }
}