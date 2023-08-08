using Microsoft.AspNetCore.Html;

namespace Mithril.Themes.Abstractions.Interfaces
{
    /// <summary>
    /// Resource interface
    /// </summary>
    public interface IResource
    {
        /// <summary>
        /// Gets the location.
        /// </summary>
        /// <value>The location.</value>
        string? Location { get; }

        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        int Order { get; }

        /// <summary>
        /// Gets the content of the resource as an IHtmlContent item.
        /// </summary>
        /// <returns>The content.</returns>
        IHtmlContent GetHtmlContent();
    }
}