using Mithril.Admin.Abstractions.BaseClasses;
using Mithril.Admin.Abstractions.Interfaces;
using Mithril.Admin.Abstractions.Services;
using Mithril.Data.Abstractions.Services;
using Mithril.Themes.Abstractions.Services;
using Mithril.Themes.Admin.ViewModels;
using System.Dynamic;
using System.Security.Claims;

namespace Mithril.Themes.Admin
{
    /// <summary>
    /// Theme editor
    /// TODO: Add tests
    /// </summary>
    /// <seealso cref="SettingsEditorBaseClass&lt;ThemeSettingsVM&gt;"/>
    public class ThemeEditor : SettingsEditorBaseClass<ThemeSettingsVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThemeEditor"/> class.
        /// </summary>
        /// <param name="themeService">The theme service.</param>
        /// <param name="dataService">The data service.</param>
        /// <param name="entityMetadataService">The entity metadata service.</param>
        public ThemeEditor(IThemeService themeService, IDataService dataService, IEntityMetadataService entityMetadataService)
            : base(dataService, entityMetadataService)
        {
            ThemeService = themeService;
        }

        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <value>The icon.</value>
        public override string Icon { get; } = "fas fa-palette";

        /// <summary>
        /// Gets the theme service.
        /// </summary>
        /// <value>The theme service.</value>
        private IThemeService ThemeService { get; }

        /// <summary>
        /// Loads the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="model">The model.</param>
        /// <param name="currentUser"></param>
        /// <returns>The entity specified.</returns>
        public override IEntity? Load(long id, ExpandoObject? model, ClaimsPrincipal? currentUser) => new ThemeSettingsVM(ThemeService);

        /// <summary>
        /// Saves the entity asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="currentUser">The current user.</param>
        /// <returns></returns>
        protected override async Task<bool> SaveEntityAsync(long id, ThemeSettingsVM? entity, ClaimsPrincipal? currentUser)
        {
            if (entity is null)
                return false;
            Abstractions.Interfaces.ITheme? DesiredTheme = ThemeService.LoadTheme(entity.CurrentTheme);
            await ThemeService.SetDefaultThemeAsync(DesiredTheme, currentUser).ConfigureAwait(false);
            return true;
        }
    }
}