using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mithril.Themes.Abstractions.Services;
using Mithril.Themes.LocationExpanders.Interfaces;

namespace Mithril.Themes.LocationExpanders
{
    /// <summary>
    /// Module view location expander provider
    /// </summary>
    /// <seealso cref="IViewLocationExpanderProvider"/>
    public class ModuleViewLocationExpanderProvider : IViewLocationExpanderProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleViewLocationExpanderProvider"/> class.
        /// </summary>
        /// <param name="themeManager">The theme manager.</param>
        public ModuleViewLocationExpanderProvider(IThemeService? themeManager)
        {
            ThemeManager = themeManager;
        }

        /// <summary>
        /// Gets the theme manager.
        /// </summary>
        /// <value>The theme manager.</value>
        private static IThemeService? ThemeManager;

        /// <summary>
        /// Gets the priority.
        /// </summary>
        /// <value>The priority.</value>
        public int Priority { get; } = 5;

        /// <inheritdoc/>
        public virtual IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context,
                                                               IEnumerable<string> viewLocations)
        {
            if (context is null)
                return Array.Empty<string>();
            viewLocations ??= Array.Empty<string>();
            if (context.ActionContext.ActionDescriptor is PageActionDescriptor page)
            {
                var pageViewLocations = PageViewLocations().ToList();
                pageViewLocations.AddRange(viewLocations);
                return pageViewLocations;

                IEnumerable<string> PageViewLocations()
                {
                    if (page.RelativePath.Contains("/Pages/") && !page.RelativePath.StartsWith("/Pages/", StringComparison.Ordinal))
                    {
                        yield return string.Concat(page.RelativePath.AsSpan(0, page.RelativePath.IndexOf("/Pages/", StringComparison.Ordinal)), "/Views/Shared/{0}", RazorViewEngine.ViewExtension);
                    }
                }
            }

            var result = new List<string>();
            if (context.ViewName.Equals("_Layout", StringComparison.Ordinal))
            {
                Abstractions.Interfaces.ITheme? CurrentTheme = ThemeManager?.LoadTheme();
                if (CurrentTheme is not null)
                {
                    var extensionViewsPath = $"/Views/Shared/_{CurrentTheme.Name}Layout" + RazorViewEngine.ViewExtension;
                    result.Add(extensionViewsPath);
                }
            }
            result.AddRange(viewLocations);

            return result;
        }

        /// <inheritdoc/>
        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }
    }
}