using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mithril.Core.Abstractions.Extensions;
using Mithril.Core.Abstractions.Modules.BaseClasses;
using Mithril.Data.Abstractions.Services;
using Mithril.Themes.Abstractions.Interfaces;
using Mithril.Themes.Abstractions.Services;
using Mithril.Themes.LocationExpanders;
using Mithril.Themes.LocationExpanders.Interfaces;
using Mithril.Themes.Models;
using Mithril.Themes.Services;

namespace Mithril.Themes
{
    /// <summary>
    /// Themes module
    /// </summary>
    /// <seealso cref="ModuleBaseClass&lt;ThemesModule&gt;"/>
    public class ThemesModule : ModuleBaseClass<ThemesModule>
    {
        /// <summary>
        /// Configures the services for the module.
        /// </summary>
        /// <param name="services">The services collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>Services</returns>
        public override IServiceCollection? ConfigureServices(IServiceCollection? services, IConfiguration? configuration, IHostEnvironment? environment)
        {
            //View location expander providers.
            return services?.AddScoped<IViewLocationExpanderProvider, DefaultViewLocationExpanderProvider>()
                            .AddScoped<IViewLocationExpanderProvider, ModuleViewLocationExpanderProvider>()

                            //Add theme service and themes
                            .AddAllSingleton<ITheme>()
                           ?.AddSingleton<IThemeService, ThemeService>()

                           //Add the view location expanders to the razor engine.
                           ?.Configure<RazorViewEngineOptions>(options => options.ViewLocationExpanders.Add(new CompositeViewLocationExpanderProvider()))

                           //Add the resource services
                           .AddScoped<IResourceService, ResourceService>();
        }

        /// <summary>
        /// Initializes the data.
        /// </summary>
        /// <param name="dataService">The data service.</param>
        /// <param name="services">The services for the application.</param>
        public override async Task InitializeDataAsync(IDataService? dataService, IServiceProvider? services)
        {
            if (!services.Exists<ITheme>())
                return;
            IEnumerable<ITheme> AvailableThemes = services?.GetServices<ITheme>() ?? Array.Empty<ITheme>();
            IEnumerable<Theme> ExistingThemes = Theme.All(dataService);

            // Create missing available themes
            foreach (ITheme MissingItems in AvailableThemes.Where<ITheme>(x => !CheckExists(x, ExistingThemes)))
            {
                var TempTheme = new Theme(MissingItems.Name);
                await TempTheme.SaveAsync(dataService, null).ConfigureAwait(false);
            }

            // Inactivate missing themes
            foreach (Theme? OldTheme in ExistingThemes.Where(x => !CheckExists(x, AvailableThemes)))
            {
                OldTheme.IsDefault = false;
                await OldTheme.DeleteAsync(dataService, null).ConfigureAwait(false);
            }

            // Reactivate any inactive themes that exist
            foreach (Theme? OldTheme in ExistingThemes.Where(x => !x.Active && CheckExists(x, AvailableThemes)))
            {
                OldTheme.Active = true;
                await OldTheme.SaveAsync(dataService, null).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Checks if a theme exists in the database.
        /// </summary>
        /// <param name="theme">The theme.</param>
        /// <param name="existingThemes">The existing themes.</param>
        /// <returns>True if it exists, false otherwise.</returns>
        private static bool CheckExists(ITheme theme, IEnumerable<Theme> existingThemes) => existingThemes.Any(y => string.Equals(y.Name, theme.Name, StringComparison.OrdinalIgnoreCase));

        /// <summary>
        /// Checks if a theme exists in the system.
        /// </summary>
        /// <param name="theme">The theme.</param>
        /// <param name="existingThemes">The existing themes.</param>
        /// <returns>True if it exists, false otherwise.</returns>
        private static bool CheckExists(Theme theme, IEnumerable<ITheme> existingThemes) => existingThemes.Any(y => string.Equals(y.Name, theme.Name, StringComparison.OrdinalIgnoreCase));
    }
}