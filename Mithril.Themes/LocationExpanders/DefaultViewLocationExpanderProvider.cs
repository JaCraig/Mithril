using Microsoft.AspNetCore.Mvc.Razor;
using Mithril.Themes.LocationExpanders.Interfaces;

namespace Mithril.Themes.LocationExpanders
{
    /// <summary>
    /// Default view location expander
    /// </summary>
    /// <seealso cref="IViewLocationExpanderProvider"/>
    public class DefaultViewLocationExpanderProvider : IViewLocationExpanderProvider
    {
        /// <summary>
        /// Gets the priority.
        /// </summary>
        /// <value>The priority.</value>
        public int Priority { get; } = 0;

        /// <summary>
        /// Invoked by a <see cref="RazorViewEngine"/> to determine potential locations for a view.
        /// </summary>
        /// <param name="context">
        /// The <see cref="ViewLocationExpanderContext"/> for the current view location expansion operation.
        /// </param>
        /// <param name="viewLocations">The sequence of view locations to expand.</param>
        /// <returns>A list of expanded view locations.</returns>
        /// <inheritdoc/>
        public virtual IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
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
        /// <inheritdoc/>
        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }
    }
}