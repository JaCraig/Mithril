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
        public ModuleViewLocationExpanderProvider(IThemeService themeManager)
        {
            ThemeManager = themeManager;
        }

        /// <summary>
        /// Gets the priority.
        /// </summary>
        /// <value>The priority.</value>
        public int Priority { get; } = 5;

        /// <summary>
        /// Gets the theme manager.
        /// </summary>
        /// <value>The theme manager.</value>
        private static IThemeService ThemeManager;

        /// <inheritdoc/>
        public virtual IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context,
                                                               IEnumerable<string> viewLocations)
        {
            if (context.ActionContext.ActionDescriptor is PageActionDescriptor page)
            {
                var pageViewLocations = PageViewLocations().ToList();
                pageViewLocations.AddRange(viewLocations);
                return pageViewLocations;

                IEnumerable<string> PageViewLocations()
                {
                    if (page.RelativePath.Contains("/Pages/") && !page.RelativePath.StartsWith("/Pages/", StringComparison.Ordinal))
                    {
                        yield return page.RelativePath.Substring(0, page.RelativePath.IndexOf("/Pages/", StringComparison.Ordinal))
                            + "/Views/Shared/{0}" + RazorViewEngine.ViewExtension;
                    }
                }
            }

            var result = new List<string>();
            /*
            if (context.ViewName.Equals("_Layout", StringComparison.Ordinal)
                || context.ViewName.Equals("_AdminLayout", StringComparison.Ordinal))
            {
                var CurrentTheme = context.ViewName.Equals("_Layout", StringComparison.Ordinal) ? ThemeManager.CurrentTheme : ThemeManager.CurrentAdminTheme;
                var extensionViewsPath = $"/Views/Shared/{CurrentTheme.Name}Layout" + RazorViewEngine.ViewExtension;
                result.Add(extensionViewsPath);
                extensionViewsPath = $"/Views/{CurrentTheme.Name}Layout" + RazorViewEngine.ViewExtension;
                result.Add(extensionViewsPath);
            }
            else if (context.ViewName.Equals("_LayoutNoHeader", StringComparison.Ordinal)
                || context.ViewName.Equals("_AdminLayoutNoHeader", StringComparison.Ordinal))
            {
                var CurrentTheme = context.ViewName.Equals("_LayoutNoHeader", StringComparison.Ordinal) ? ThemeManager.CurrentTheme : ThemeManager.CurrentAdminTheme;
                var extensionViewsPath = $"/Views/Shared/{CurrentTheme.Name}LayoutNoHeader" + RazorViewEngine.ViewExtension;
                result.Add(extensionViewsPath);
                extensionViewsPath = $"/Views/{CurrentTheme.Name}LayoutNoHeader" + RazorViewEngine.ViewExtension;
                result.Add(extensionViewsPath);
            }
            */
            result.AddRange(viewLocations);

            return result;
        }

        /// <inheritdoc/>
        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }
    }
}