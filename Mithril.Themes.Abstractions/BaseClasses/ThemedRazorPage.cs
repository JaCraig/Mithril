using BigBook;
using Mithril.Core.Abstractions.Mvc.Context;
using Mithril.Themes.Abstractions.Interfaces;
using Mithril.Themes.Abstractions.Services;

namespace Mithril.Themes.Abstractions.BaseClasses
{
    /// <summary>
    /// Themed Razor page
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Razor.RazorPage&lt;TModel&gt;" />
    public abstract class ThemedRazorPage<TModel> : Microsoft.AspNetCore.Mvc.Razor.RazorPage<TModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThemedRazorPage{TModel}"/> class.
        /// </summary>
        protected ThemedRazorPage()
        {
            ResourceHolder = GetService<IResourceService>();
            ThemeService = GetService<IThemeService>();
            CurrentTheme = ThemeService?.LoadTheme();
        }

        /// <summary>
        /// Gets the theme service.
        /// </summary>
        /// <value>The theme service.</value>
        public IThemeService? ThemeService { get; }

        /// <summary>
        /// Gets the current theme.
        /// </summary>
        /// <value>The current theme.</value>
        protected ITheme? CurrentTheme { get; }

        /// <summary>
        /// Gets the resource holder.
        /// </summary>
        /// <value>The resource holder.</value>
        protected IResourceService? ResourceHolder { get; }

        /// <summary>
        /// Executes the page asynchronously.
        /// </summary>
        /// <returns>The async task.</returns>
        public override Task ExecuteAsync()
        {
            if (CurrentTheme is null)
            {
                return Task.CompletedTask;
            }

            Layout = (CurrentTheme?.Name.Remove("\\s") ?? "_") + "Layout.cshtml";
            return Task.CompletedTask;
        }

        /// <summary>
        /// Gets a service.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <returns>The service specified.</returns>
        protected TService? GetService<TService>()
        {
            return (TService?)HttpContext.Current?.RequestServices.GetService(typeof(TService));
        }
    }
}