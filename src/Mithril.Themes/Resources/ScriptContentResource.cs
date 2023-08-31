using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mithril.Themes.Abstractions.Interfaces;

namespace Mithril.Themes.Resources
{
    /// <summary>
    /// Script content resource
    /// </summary>
    /// <seealso cref="IResource"/>
    /// <seealso cref="IEquatable{ScriptContentResource}"/>
    public class ScriptContentResource : IResource, IEquatable<ScriptContentResource>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptContentResource" /> class.
        /// </summary>
        /// <param name="content">The content.</param>
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
        public ScriptContentResource(string? content, string? async, string? charset, string? defer, string? type, string? xMLSpace, int tagOrder, string? location, string? integrity, string? crossOrigin, string? referrerPolicy)
        {
            Order = tagOrder;
            Integrity = integrity ?? "";
            CrossOrigin = crossOrigin ?? "";
            ReferrerPolicy = referrerPolicy ?? "";
            Content = content ?? "";
            Async = async ?? "";
            Charset = charset ?? "";
            Defer = string.Equals(defer, "TRUE", StringComparison.OrdinalIgnoreCase);
            Type = type ?? "";
            XMLSpace = xMLSpace ?? "";
            Location = string.IsNullOrEmpty(location) ? "Footer" : location;
        }

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <value>The asynchronous.</value>
        public string Async { get; }

        /// <summary>
        /// Gets the charset.
        /// </summary>
        /// <value>The charset.</value>
        public string Charset { get; }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>The source.</value>
        public string Content { get; set; }

        /// <summary>
        /// Gets the crossorigin.
        /// </summary>
        /// <value>
        /// The crossorigin.
        /// </value>
        public string CrossOrigin { get; }

        /// <summary>
        /// Gets the defer.
        /// </summary>
        /// <value>The defer.</value>
        public bool Defer { get; }

        /// <summary>
        /// Gets the integrity.
        /// </summary>
        /// <value>
        /// The integrity.
        /// </value>
        public string Integrity { get; }

        /// <summary>
        /// Gets the position.
        /// </summary>
        /// <value>The position.</value>
        public string Location { get; }

        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        public int Order { get; set; }

        /// <summary>
        /// Gets the referrerpolicy.
        /// </summary>
        /// <value>
        /// The referrerpolicy.
        /// </value>
        public string ReferrerPolicy { get; }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        public string Type { get; }

        /// <summary>
        /// Gets the XML space.
        /// </summary>
        /// <value>The XML space.</value>
        public string XMLSpace { get; }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="resource1">The resource1.</param>
        /// <param name="resource2">The resource2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(ScriptContentResource? resource1, ScriptContentResource? resource2) => !(resource1 == resource2);

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="resource1">The resource1.</param>
        /// <param name="resource2">The resource2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(ScriptContentResource? resource1, ScriptContentResource? resource2) => EqualityComparer<ScriptContentResource>.Default.Equals(resource1, resource2);

        /// <summary>
        /// Determines whether the specified <see cref="object"/>, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object? obj) => Equals(obj as ScriptContentResource);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref>
        /// parameter; otherwise, false.
        /// </returns>
        public bool Equals(ScriptContentResource? other)
        {
            return other != null
                   && Content == other.Content;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data
        /// structures like a hash table.
        /// </returns>
        public override int GetHashCode() => 1997410482 + EqualityComparer<string>.Default.GetHashCode(Content);

        /// <summary>
        /// Gets the content of the resource as an IHtmlContent item.
        /// </summary>
        /// <returns>The content.</returns>
        public IHtmlContent GetHtmlContent()
        {
            var Builder = new TagBuilder("script");
            MergeAttribute(Builder, "async", Async);
            MergeAttribute(Builder, "async", Async);
            MergeAttribute(Builder, "charset", Charset);
            MergeAttribute(Builder, "defer", Defer);
            MergeAttribute(Builder, "type", Type);
            MergeAttribute(Builder, "xml:space", XMLSpace);
            MergeAttribute(Builder, "integrity", Integrity);
            MergeAttribute(Builder, "crossorigin", CrossOrigin);
            MergeAttribute(Builder, "referrerpolicy", ReferrerPolicy);
            _ = Builder.InnerHtml.AppendHtml(Content);
            return Builder;
        }

        /// <summary>
        /// Merges the attribute.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        private static void MergeAttribute(TagBuilder builder, string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
                builder.MergeAttribute(key, value);
        }

        /// <summary>
        /// Merges the attribute.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        private static void MergeAttribute(TagBuilder builder, string key, bool value)
        {
            if (value)
                builder.MergeAttribute(key, "");
        }
    }
}