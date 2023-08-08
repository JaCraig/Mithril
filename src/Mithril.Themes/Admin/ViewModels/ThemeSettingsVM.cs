using Mithril.Admin.Abstractions.BaseClasses;
using Mithril.Admin.Abstractions.DataEditor.Attributes;
using Mithril.Admin.Abstractions.Interfaces;
using Mithril.Data.Abstractions.Services;
using Mithril.Themes.Abstractions.Services;
using Mithril.Themes.Admin.DropDowns;
using Mithril.Themes.Models;

namespace Mithril.Themes.Admin.ViewModels
{
    /// <summary>
    /// Theme settings VM
    /// </summary>
    /// <seealso cref="IEntity" />
    public class ThemeSettingsVM : SettingsBaseClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThemeSettingsVM"/> class.
        /// </summary>
        public ThemeSettingsVM()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThemeSettingsVM" /> class.
        /// </summary>
        /// <param name="themes">The themes.</param>
        /// <param name="dataService">The data service.</param>
        public ThemeSettingsVM(IThemeService? themes, IDataService? dataService)
        {
            if (themes is null)
                return;
            CurrentTheme = Theme.Load(themes.LoadTheme()?.Name ?? "", dataService)?.ID ?? 0;
        }

        /// <summary>
        /// Gets or sets the current theme.
        /// </summary>
        /// <value>
        /// The current theme.
        /// </value>
        [Select(typeof(ThemeList))]
        public long CurrentTheme { get; set; }
    }
}