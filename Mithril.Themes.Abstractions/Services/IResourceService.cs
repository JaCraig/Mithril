using Microsoft.AspNetCore.Html;
using Mithril.Themes.Abstractions.Enums;
using Mithril.Themes.Abstractions.Interfaces;

namespace Mithril.Themes.Abstractions.Services
{
    /// <summary>
    /// Resource service interface
    /// </summary>
    public interface IResourceService
    {
        /// <summary>
        /// Adds the link resource.
        /// </summary>
        /// <param name="href">The href.</param>
        /// <param name="rel">The relative.</param>
        /// <param name="type">The type.</param>
        /// <param name="crossOrigin">The cross origin.</param>
        /// <param name="hreflang">The hreflang.</param>
        /// <param name="media">The media.</param>
        /// <param name="sizes">The sizes.</param>
        /// <param name="charset">The charset.</param>
        /// <param name="rev">The rev.</param>
        /// <param name="target">The target.</param>
        /// <param name="tagOrder">The tag order.</param>
        /// <param name="location">The location.</param>
        /// <returns>True if it is successfully added, false otherwise.</returns>
        bool AddLinkResource(string? href, string? rel, string? type, string? crossOrigin, string? hreflang, string? media, string? sizes, string? charset, string? rev, string? target, int tagOrder, string? location);

        /// <summary>
        /// Adds a meta resource.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="scheme">The scheme.</param>
        /// <param name="httpEquiv">The HTTP equiv.</param>
        /// <param name="content">The content.</param>
        /// <param name="charset">The charset.</param>
        /// <param name="tagOrder">The tag order.</param>
        /// <returns>True if it is successfully added, false otherwise.</returns>
        bool AddMetaResource(string? name, string? scheme, string? httpEquiv, string? content, string? charset, string? property, int tagOrder);

        /// <summary>
        /// Adds the resource.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="resource">The resource.</param>
        /// <returns>True if it is successfully added, false otherwise.</returns>
        bool AddResource(ResourceType type, IResource resource);

        /// <summary>
        /// Adds the script content resource.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="async">The asynchronous.</param>
        /// <param name="charset">The charset.</param>
        /// <param name="defer">The defer.</param>
        /// <param name="type">The type.</param>
        /// <param name="xMLSpace">The xml:space.</param>
        /// <param name="tagOrder">The tag order.</param>
        /// <param name="location">The location.</param>
        /// <returns>True if it is successfully added, false otherwise.</returns>
        bool AddScriptContentResource(string? content, string? async, string? charset, string? defer, string? type, string? xMLSpace, int tagOrder, string? location);

        /// <summary>
        /// Adds the script file resource.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="async">The asynchronous.</param>
        /// <param name="charset">The charset.</param>
        /// <param name="defer">The defer.</param>
        /// <param name="type">The type.</param>
        /// <param name="xMLSpace">The x ml space.</param>
        /// <param name="tagOrder">The tag order.</param>
        /// <param name="location">The location.</param>
        /// <returns>True if it is successfully added, false otherwise.</returns>
        bool AddScriptFileResource(string? file, string? async, string? charset, string? defer, string? type, string? xMLSpace, int tagOrder, string? location);

        /// <summary>
        /// Adds the style resource.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="media">The media.</param>
        /// <param name="type">The type.</param>
        /// <param name="tagOrder">The tag order.</param>
        /// <returns>True if it is successfully added, false otherwise.</returns>
        bool AddStyleResource(string? content, string? media, string? type, int tagOrder, string? location);

        /// <summary>
        /// Nexts the order value.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        int NextOrderValue(ResourceType type);

        /// <summary>
        /// Renders the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="location">The location.</param>
        /// <returns>The HTML content.</returns>
        IHtmlContent Render(ResourceType type, string location = "");
    }
}