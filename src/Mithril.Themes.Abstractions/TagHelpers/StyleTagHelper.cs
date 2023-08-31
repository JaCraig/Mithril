using Microsoft.AspNetCore.Razor.TagHelpers;
using Mithril.Themes.Abstractions.Enums;
using Mithril.Themes.Abstractions.Services;
using System.Globalization;

namespace Mithril.Themes.Abstractions.TagHelpers
{
    /// <summary>
    /// Style tag helper
    /// </summary>
    /// <seealso cref="TagHelper"/>
    public class StyleTagHelper : TagHelper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StyleTagHelper"/> class.
        /// </summary>
        /// <param name="resources">The resources.</param>
        public StyleTagHelper(IResourceService? resources)
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
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (context is null || output is null)
                return;
            var TagOrder = context.AllAttributes.ContainsName("order")
                ? int.Parse(context.AllAttributes["order"].Value.ToString() ?? "0", CultureInfo.InvariantCulture)
                : (Resources?.NextOrderValue(ResourceType) ?? 0);
            var Content = (await output.GetChildContentAsync().ConfigureAwait(false)).GetContent();
            var Media = GetValue(context, "media");
            var Type = GetValue(context, "type");
            var Location = GetValue(context, "location");
            _ = (Resources?.AddStyleResource(Content, Media, Type, TagOrder, Location));
            output.SuppressOutput();
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="name">The name.</param>
        /// <returns>The value.</returns>
        private static string? GetValue(TagHelperContext context, string name) => context.AllAttributes.ContainsName(name) ? context.AllAttributes[name].Value.ToString() : "";
    }
}