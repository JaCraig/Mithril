using BigBook;
using Microsoft.AspNetCore.Html;
using Mithril.Themes.Abstractions.Enums;
using Mithril.Themes.Abstractions.Interfaces;
using Mithril.Themes.Abstractions.Services;
using Mithril.Themes.Resources;

namespace Mithril.Themes.Services
{
    /// <summary>
    /// Resource service
    /// </summary>
    /// <seealso cref="IResourceService" />
    public class ResourceService : IResourceService
    {
        /// <summary>
        /// Gets or sets the resources.
        /// </summary>
        /// <value>The resources.</value>
        public ListMapping<ResourceType, IResource> Resources { get; } = new ListMapping<ResourceType, IResource>();

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
        public bool AddLinkResource(string? href, string? rel, string? type, string? crossOrigin, string? hreflang, string? media, string? sizes, string? charset, string? rev, string? target, int tagOrder, string? location)
        {
            return AddResource(ResourceType.CSS, new LinkResource(href, charset, crossOrigin, hreflang, media, rel, rev, sizes, target, type, tagOrder, location));
        }

        /// <summary>
        /// Adds the meta resource.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="scheme">The scheme.</param>
        /// <param name="httpEquiv">The HTTP equiv.</param>
        /// <param name="content">The content.</param>
        /// <param name="charset">The charset.</param>
        /// <param name="property">The property.</param>
        /// <param name="tagOrder">The tag order.</param>
        /// <returns>True if it is successfully added, false otherwise.</returns>
        public bool AddMetaResource(string? name, string? scheme, string? httpEquiv, string? content, string? charset, string? property, int tagOrder)
        {
            return AddResource(ResourceType.Meta, new MetaResource(name, scheme, httpEquiv, content, charset, property, tagOrder));
        }

        /// <summary>
        /// Adds the resource.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="resource">The resource.</param>
        /// <returns>True if it is successfully added, false otherwise.</returns>
        public bool AddResource(ResourceType type, IResource resource)
        {
            if (resource is null)
                return false;
            if (!Resources.ContainsKey(type) || !Resources[type].Contains(resource))
            {
                Resources.Add(type, resource);
            }
            return true;
        }

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
        /// <param name="location">The position.</param>
        /// <param name="integrity">The integrity.</param>
        /// <param name="crossOrigin">The cross origin.</param>
        /// <param name="referrerPolicy">The referrer policy.</param>
        /// <returns>
        /// True if it is successfully added, false otherwise.
        /// </returns>
        public bool AddScriptContentResource(string? content, string? async, string? charset, string? defer, string? type, string? xMLSpace, int tagOrder, string? location, string? integrity, string? crossOrigin, string? referrerPolicy)
        {
            return AddResource(ResourceType.JavaScript, new ScriptContentResource(content, async, charset, defer, type, xMLSpace, tagOrder, location, integrity, crossOrigin, referrerPolicy));
        }

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
        /// <param name="location">The position.</param>
        /// <param name="integrity">The integrity.</param>
        /// <param name="crossOrigin">The cross origin.</param>
        /// <param name="referrerPolicy">The referrer policy.</param>
        /// <returns>
        /// True if it is successfully added, false otherwise.
        /// </returns>
        public bool AddScriptFileResource(string? file, string? async, string? charset, string? defer, string? type, string? xMLSpace, int tagOrder, string? location, string? integrity, string? crossOrigin, string? referrerPolicy)
        {
            return AddResource(ResourceType.JavaScript, new ScriptFileResource(file, async, charset, defer, type, xMLSpace, tagOrder, location, integrity, crossOrigin, referrerPolicy));
        }

        /// <summary>
        /// Adds the style resource.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="media">The media.</param>
        /// <param name="type">The type.</param>
        /// <param name="tagOrder">The tag order.</param>
        /// <param name="location"></param>
        /// <returns>True if it is successfully added, false otherwise.</returns>
        public bool AddStyleResource(string? content, string? media, string? type, int tagOrder, string? location)
        {
            return AddResource(ResourceType.CSS, new StyleResource(content, media, type, tagOrder, location));
        }

        /// <summary>
        /// Nexts the order value.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The next order value</returns>
        public int NextOrderValue(ResourceType type)
        {
            return Resources.ContainsKey(type) ? Resources[type].Count() : 0;
        }

        /// <summary>
        /// Renders the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="location">The location.</param>
        /// <returns>The HTML content.</returns>
        public IHtmlContent Render(ResourceType type, string location = "")
        {
            location ??= "";
            var Result = new HtmlContentBuilder();
            Result.AppendHtml(Environment.NewLine);
            foreach (var Resource in Resources[type]
                .Where(x => string.IsNullOrEmpty(location) || string.Equals(x.Location, location, StringComparison.OrdinalIgnoreCase))
                .OrderBy(x => x.Order))
            {
                Result.AppendHtml(Resource.GetHtmlContent());
            }
            return Result;
        }
    }
}