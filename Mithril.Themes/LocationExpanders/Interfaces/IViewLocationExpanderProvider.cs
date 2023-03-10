using Microsoft.AspNetCore.Mvc.Razor;

namespace Mithril.Themes.LocationExpanders.Interfaces
{
    /// <summary>
    /// View location expander provider interface
    /// </summary>
    /// <seealso cref="IViewLocationExpander"/>
    public interface IViewLocationExpanderProvider : IViewLocationExpander
    {
        /// <summary>
        /// Gets the priority.
        /// </summary>
        /// <value>The priority.</value>
        int Priority { get; }
    }
}