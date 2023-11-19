using Microsoft.AspNetCore.Razor.TagHelpers;
using Mithril.Themes.Abstractions.Enums;
using Mithril.Themes.Abstractions.Services;
using System.Globalization;

namespace Mithril.Themes.Abstractions.TagHelpers
{
    /// <summary>
    /// Meta tag helper
    /// </summary>
    /// <seealso cref="TagHelper"/>
    /// <remarks>
    /// Initializes a new instance of the <see cref="MetaTagHelper"/> class.
    /// </remarks>
    /// <param name="resources">The resources.</param>
    public class MetaTagHelper(IResourceService? resources) : TagHelper
    {
        /// <summary>
        /// Gets the resources.
        /// </summary>
        /// <value>The resources.</value>
        public IResourceService? Resources { get; } = resources;

        /// <summary>
        /// Gets the type of the resource.
        /// </summary>
        /// <value>The type of the resource.</value>
        private static ResourceType ResourceType { get; } = ResourceType.Meta;

        /// <summary>
        /// Synchronously executes the <see cref="TagHelper"/> with the given context and output.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (context is null || output is null)
                return Task.CompletedTask;
            var Name = GetValue(context, "name");
            var Scheme = GetValue(context, "scheme");
            var HttpEquiv = GetValue(context, "http-equiv");
            var Content = GetValue(context, "content");
            var Charset = GetValue(context, "charset");
            var Property = GetValue(context, "property");
            var TagOrder = context.AllAttributes.ContainsName("order")
                ? int.Parse(context.AllAttributes["order"].Value.ToString() ?? "0", CultureInfo.InvariantCulture)
                : (Resources?.NextOrderValue(ResourceType) ?? 0);
            _ = (Resources?.AddMetaResource(Name, Scheme, HttpEquiv, Content, Charset, Property, TagOrder));
            output.SuppressOutput();
            return Task.CompletedTask;
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