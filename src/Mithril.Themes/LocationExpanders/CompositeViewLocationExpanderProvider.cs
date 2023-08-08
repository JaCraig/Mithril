using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Mithril.Themes.LocationExpanders.Interfaces;

namespace Mithril.Themes.LocationExpanders
{
    /// <summary>
    /// Composite view location expander provider.
    /// </summary>
    /// <seealso cref="IViewLocationExpanderProvider"/>
    public class CompositeViewLocationExpanderProvider : IViewLocationExpanderProvider
    {
        /// <summary>
        /// Gets the priority.
        /// </summary>
        /// <value>The priority.</value>
        public int Priority { get; }

        /// <summary>
        /// Invoked by a <see cref="RazorViewEngine"/> to determine potential locations for a view.
        /// </summary>
        /// <param name="context">
        /// The <see cref="ViewLocationExpanderContext"/> for the current view location expansion operation.
        /// </param>
        /// <param name="viewLocations">The sequence of view locations to expand.</param>
        /// <returns>A list of expanded view locations.</returns>
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            foreach (var provider in DiscoverProviders(context))
            {
                viewLocations = provider.ExpandViewLocations(context, viewLocations);
            }
            return viewLocations;
        }

        /// <summary>
        /// Invoked by a <see cref="RazorViewEngine"/> to determine the values that would be
        /// consumed by this instance of <see cref="IViewLocationExpander"/>. The calculated values
        /// are used to determine if the view location has changed since the last time it was located.
        /// </summary>
        /// <param name="context">
        /// The <see cref="ViewLocationExpanderContext"/> for the current view location expansion operation.
        /// </param>
        public void PopulateValues(ViewLocationExpanderContext context)
        {
            foreach (var provider in DiscoverProviders(context))
            {
                provider.PopulateValues(context);
            }
        }

        /// <summary>
        /// Discovers the providers.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>The list of providers.</returns>
        private static IEnumerable<IViewLocationExpanderProvider> DiscoverProviders(ViewLocationExpanderContext context)
        {
            if (context is null)
                return Array.Empty<IViewLocationExpanderProvider>();
            return context
                .ActionContext
                .HttpContext
                .RequestServices
                .GetServices<IViewLocationExpanderProvider>()
                .OrderBy(x => x.Priority);
        }
    }
}