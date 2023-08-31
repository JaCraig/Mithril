using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mithril.Themes.Abstractions.Interfaces;

namespace Mithril.Themes.Resources
{
    /// <summary>
    /// Style resource
    /// </summary>
    /// <seealso cref="IEquatable{StyleResource}"/>
    /// <seealso cref="IResource"/>
    public class StyleResource : IResource, IEquatable<StyleResource>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StyleResource"/> class.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="media">The media.</param>
        /// <param name="type">The type.</param>
        /// <param name="order">The order.</param>
        /// <param name="location">The location.</param>
        public StyleResource(string? content, string? media, string? type, int order, string? location)
        {
            Content = content ?? "";
            Order = order;
            Media = media;
            Type = type;
            Location = string.IsNullOrEmpty(location) ? "Header" : location;
        }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>The source.</value>
        public string Content { get; set; }

        /// <summary>
        /// Gets the location.
        /// </summary>
        /// <value>The location.</value>
        public string? Location { get; }

        /// <summary>
        /// Gets or sets the media.
        /// </summary>
        /// <value>The media.</value>
        public string? Media { get; set; }

        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public string? Type { get; set; }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="resource1">The resource1.</param>
        /// <param name="resource2">The resource2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(StyleResource? resource1, StyleResource? resource2) => !(resource1 == resource2);

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="resource1">The resource1.</param>
        /// <param name="resource2">The resource2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(StyleResource? resource1, StyleResource? resource2) => EqualityComparer<StyleResource>.Default.Equals(resource1, resource2);

        /// <summary>
        /// Determines whether the specified <see cref="object"/>, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object? obj) => Equals(obj as StyleResource);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref>
        /// parameter; otherwise, false.
        /// </returns>
        public bool Equals(StyleResource? other)
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
            var Builder = new TagBuilder("style");
            MergeAttribute(Builder, "type", Type);
            MergeAttribute(Builder, "media", Media);
            _ = Builder.InnerHtml.AppendHtml(Content);
            return Builder;
        }

        /// <summary>
        /// Merges the attribute.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        private static void MergeAttribute(TagBuilder builder, string key, string? value)
        {
            if (!string.IsNullOrEmpty(value))
                builder.MergeAttribute(key, value);
        }
    }
}