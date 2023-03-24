using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mithril.Themes.Abstractions.Interfaces;

namespace Mithril.Themes.Resources
{
    /// <summary>
    /// Meta resource
    /// </summary>
    /// <seealso cref="IResource"/>
    /// <seealso cref="IEquatable{MetaResource}"/>
    public class MetaResource : IResource, IEquatable<MetaResource>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MetaResource"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="scheme">The schema.</param>
        /// <param name="httpEquiv">The HTTP equiv.</param>
        /// <param name="content">The content.</param>
        /// <param name="charset">The charset.</param>
        /// <param name="property">The property.</param>
        /// <param name="order">The order.</param>
        public MetaResource(string? name, string? scheme, string? httpEquiv, string? content, string? charset, string? property, int order)
        {
            Name = name;
            Scheme = scheme;
            HttpEquiv = httpEquiv;
            Content = content;
            Charset = charset;
            Order = order;
            Property = property;
        }

        /// <summary>
        /// Gets the charset.
        /// </summary>
        /// <value>The charset.</value>
        public string? Charset { get; }

        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <value>The content.</value>
        public string? Content { get; }

        /// <summary>
        /// Gets the HTTP equiv.
        /// </summary>
        /// <value>The HTTP equiv.</value>
        public string? HttpEquiv { get; }

        /// <summary>
        /// Gets the location.
        /// </summary>
        /// <value>The location.</value>
        public string Location { get; } = "Header";

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string? Name { get; }

        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        public int Order { get; set; }

        /// <summary>
        /// Gets the property.
        /// </summary>
        /// <value>The property.</value>
        public string? Property { get; }

        /// <summary>
        /// Gets the schema.
        /// </summary>
        /// <value>The schema.</value>
        public string? Scheme { get; }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="resource1">The resource1.</param>
        /// <param name="resource2">The resource2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(MetaResource? resource1, MetaResource? resource2)
        {
            return !(resource1 == resource2);
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="resource1">The resource1.</param>
        /// <param name="resource2">The resource2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(MetaResource? resource1, MetaResource? resource2)
        {
            return EqualityComparer<MetaResource>.Default.Equals(resource1, resource2);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/>, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object? obj)
        {
            return Equals(obj as MetaResource);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref>
        /// parameter; otherwise, false.
        /// </returns>
        public bool Equals(MetaResource? other)
        {
            return other != null
                   && Charset == other.Charset
                   && Content == other.Content
                   && HttpEquiv == other.HttpEquiv
                   && Name == other.Name
                   && Property == other.Property
                   && Scheme == other.Scheme;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data
        /// structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            var hashCode = -2081355158;
            hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(Charset);
            hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(Content);
            hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(HttpEquiv);
            hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(Property);
            return (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(Scheme);
        }

        /// <summary>
        /// Gets the content of the resource as an IHtmlContent item.
        /// </summary>
        /// <returns>The content.</returns>
        public IHtmlContent GetHtmlContent()
        {
            var Builder = new TagBuilder("meta");
            MergeAttribute(Builder, "charset", Charset);
            MergeAttribute(Builder, "content", Content);
            MergeAttribute(Builder, "http-equiv", HttpEquiv);
            MergeAttribute(Builder, "name", Name);
            MergeAttribute(Builder, "scheme", Scheme);
            MergeAttribute(Builder, "property", Property);
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