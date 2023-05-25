using Mithril.Admin.Abstractions.DataEditor.Attributes;
using Mithril.Admin.Abstractions.Interfaces;
using Mithril.Themes.Abstractions.Services;
using Mithril.Themes.Admin.DropDowns;

namespace Mithril.Themes.Admin.ViewModels
{
    /// <summary>
    /// Theme settings VM
    /// TODO: Add tests
    /// </summary>
    /// <seealso cref="IEntity" />
    public class ThemeSettingsVM : IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThemeSettingsVM"/> class.
        /// </summary>
        public ThemeSettingsVM()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThemeSettingsVM"/> class.
        /// </summary>
        /// <param name="themes">The themes.</param>
        public ThemeSettingsVM(IThemeService? themes)
        {
            if (themes is null)
                return;
            CurrentTheme = themes.LoadTheme()?.Name ?? "";
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:Mithril.Admin.Abstractions.Interfaces.IEntity" /> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the current theme.
        /// </summary>
        /// <value>
        /// The current theme.
        /// </value>
        [DropDown(typeof(ThemeList))]
        public string? CurrentTheme { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public long ID { get; set; }
    }
}