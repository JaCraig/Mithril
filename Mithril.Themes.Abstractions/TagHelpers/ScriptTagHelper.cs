using Microsoft.AspNetCore.Razor.TagHelpers;
using Mithril.Themes.Abstractions.Enums;
using Mithril.Themes.Abstractions.Services;
using System.Globalization;

namespace Mithril.Themes.Abstractions.TagHelpers
{
    /// <summary>
    /// Resource tag helper
    /// </summary>
    /// <seealso cref="TagHelper"/>
    public class ScriptTagHelper : TagHelper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptTagHelper"/> class.
        /// </summary>
        /// <param name="resources">The resources.</param>
        public ScriptTagHelper(IResourceService? resources)
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
        private static ResourceType ResourceType { get; } = ResourceType.JavaScript;

        /// <summary>
        /// Synchronously executes the <see cref="TagHelper"/> with the given values for context and output.
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
            var Async = GetValue(context, "async");
            var Charset = GetValue(context, "charset");
            var Defer = GetValue(context, "defer");
            var Src = GetValue(context, "src");
            var Type = GetValue(context, "type");
            var XMLSpace = GetValue(context, "xml:space");
            var Location = GetValue(context, "location");
            var Integrity = GetValue(context, "integrity");
            var CrossOrigin = GetValue(context, "crossorigin");
            var ReferrerPolicy = GetValue(context, "referrerpolicy");
            if (!string.IsNullOrEmpty(Src))
            {
                Resources?.AddScriptFileResource(Src, Async, Charset, Defer, Type, XMLSpace, TagOrder, Location, Integrity, CrossOrigin, ReferrerPolicy);
            }
            else
            {
                var Content = (await output.GetChildContentAsync().ConfigureAwait(false)).GetContent();
                Resources?.AddScriptContentResource(Content, Async, Charset, Defer, Type, XMLSpace, TagOrder, Location, Integrity, CrossOrigin, ReferrerPolicy);
            }
            output.SuppressOutput();
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="name">The name.</param>
        /// <returns>The value.</returns>
        private static string? GetValue(TagHelperContext context, string name)
        {
            if (name == "defer")
            {
                return context.AllAttributes.ContainsName(name) ? "true" : "";
            }
            return context.AllAttributes.ContainsName(name) ? context.AllAttributes[name].Value.ToString() : "";
        }
    }
}