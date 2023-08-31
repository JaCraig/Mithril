using Inflatable.BaseClasses;
using Mithril.Data.Abstractions.Databases;

namespace Mithril.Themes.Models.Mappings
{
    /// <summary>
    /// Theme mapping
    /// </summary>
    /// <seealso cref="MappingBaseClass&lt;Theme, DefaultDatabase&gt;"/>
    public class ThemeMapping : MappingBaseClass<Theme, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThemeMapping"/> class.
        /// </summary>
        public ThemeMapping()
        {
            _ = Reference(x => x.IsDefault);
            _ = Reference(x => x.Name);
        }
    }
}