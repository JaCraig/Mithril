using Microsoft.AspNetCore.Razor.TagHelpers;
using Mithril.Themes.Abstractions.Enums;
using Mithril.Themes.Abstractions.Services;
using System.Globalization;

namespace Mithril.Themes.Abstractions.TagHelpers
{
    /// <summary>
    /// Link tag helper
    /// </summary>
    /// <seealso cref="TagHelper"/>
    public class LinkTagHelper : TagHelper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LinkTagHelper"/> class.
        /// </summary>
        /// <param name="resources">The resources.</param>
        public LinkTagHelper(IResourceService? resources)
        {
            Resources = resources;
        }

        /// <summary>
        /// Gets the resources.
        /// </summary>
        /// <value>The resources.</value>
        public IResourceService? Resources { get; }

        /// <summary>
        /// Gets the type of the resource.
        /// </summary>
        /// <value>The type of the resource.</value>
        private static ResourceType ResourceType { get; } = ResourceType.CSS;

        /// <summary>
        /// Synchronously executes the <see cref="TagHelper"/> with the given context and output.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (context is null || output is null)
                return Task.CompletedTask;
            if (context.AllAttributes.ContainsName("href"))
            {
                var Href = context.AllAttributes["href"].Value.ToString();
                var TagOrder = context.AllAttributes.ContainsName("order")
                            ? int.Parse(context.AllAttributes["order"].Value.ToString() ?? "0", CultureInfo.InvariantCulture)
                            : (Resources?.NextOrderValue(ResourceType) ?? 0);
                var Rel = GetValue(context, "rel");
                var Type = GetValue(context, "type");
                var CrossOrigin = GetValue(context, "crossorigin");
                var Hreflang = GetValue(context, "hreflang");
                var Media = GetValue(context, "media");
                var Sizes = GetValue(context, "sizes");
                var Charset = GetValue(context, "charset");
                var Rev = GetValue(context, "rev");
                var Target = GetValue(context, "target");
                var Location = GetValue(context, "location");
                Resources?.AddLinkResource(Href, Rel, Type, CrossOrigin, Hreflang, Media, Sizes, Charset, Rev, Target, TagOrder, Location);
            }
            output.SuppressOutput();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="name">The name.</param>
        /// <returns>The value.</returns>
        private static string? GetValue(TagHelperContext context, string name)
        {
            return context.AllAttributes.ContainsName(name) ? context.AllAttributes[name].Value.ToString() : "";
        }
    }
}