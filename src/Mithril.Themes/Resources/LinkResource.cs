using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mithril.Themes.Abstractions.Interfaces;

namespace Mithril.Themes.Resources
{
    /// <summary>
    /// Link resource
    /// </summary>
    /// <seealso cref="IResource"/>
    /// <seealso cref="IEquatable{LinkResource}"/>
    public class LinkResource : IResource, IEquatable<LinkResource>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LinkResource"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="charset">The charset.</param>
        /// <param name="crossOrigin">The cross origin.</param>
        /// <param name="hrefLang">The href language.</param>
        /// <param name="media">The media.</param>
        /// <param name="rel">The relative.</param>
        /// <param name="rev">The rev.</param>
        /// <param name="sizes">The sizes.</param>
        /// <param name="target">The target.</param>
        /// <param name="type">The type.</param>
        /// <param name="order">The order.</param>
        /// <param name="location">The location.</param>
        public LinkResource(
            string? source,
            string? charset,
            string? crossOrigin,
            string? hrefLang,
            string? media,
            string? rel,
            string? rev,
            string? sizes,
            string? target,
            string? type,
            int order,
            string? location)
        {
            Source = source?.Replace("~/", "/") ?? "";
            Rel = string.IsNullOrEmpty(rel) ? "stylesheet" : rel;
            Type = string.IsNullOrEmpty(type) ? "text/css" : type;
            Order = order;
            Charset = charset ?? "";
            CrossOrigin = crossOrigin ?? "";
            HrefLang = hrefLang ?? "";
            Media = media ?? "";
            Rev = rev ?? "";
            Sizes = sizes ?? "";
            Target = target ?? "";
            Location = string.IsNullOrEmpty(location) ? "Header" : location;
            Version = string.Empty;
            if (Source.StartsWith("/", StringComparison.Ordinal))
            {
                var SourceFile = new FileCurator.FileInfo("~/wwwroot/" + Source);
                if (SourceFile.Exists)
                {
                    Version = Math.Abs(HashCode.Combine(SourceFile.Modified)).ToString();
                }
            }
        }

        /// <summary>
        /// Gets the charset.
        /// </summary>
        /// <value>The charset.</value>
        public string Charset { get; }

        /// <summary>
        /// Gets the cross origin.
        /// </summary>
        /// <value>The cross origin.</value>
        public string CrossOrigin { get; }

        /// <summary>
        /// Gets the href language.
        /// </summary>
        /// <value>The href language.</value>
        public string HrefLang { get; }

        /// <summary>
        /// Gets the location.
        /// </summary>
        /// <value>The location.</value>
        public string Location { get; }

        /// <summary>
        /// Gets the media.
        /// </summary>
        /// <value>The media.</value>
        public string Media { get; }

        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets the relative.
        /// </summary>
        /// <value>The relative.</value>
        public string Rel { get; set; }

        /// <summary>
        /// Gets the rev.
        /// </summary>
        /// <value>The rev.</value>
        public string Rev { get; }

        /// <summary>
        /// Gets the sizes.
        /// </summary>
        /// <value>The sizes.</value>
        public string Sizes { get; }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>The source.</value>
        public string Source { get; set; }

        /// <summary>
        /// Gets the target.
        /// </summary>
        /// <value>The target.</value>
        public string Target { get; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public string Type { get; set; }

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>The version.</value>
        public string Version { get; }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="resource1">The resource1.</param>
        /// <param name="resource2">The resource2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(LinkResource? resource1, LinkResource? resource2) => !(resource1 == resource2);

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="resource1">The resource1.</param>
        /// <param name="resource2">The resource2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(LinkResource? resource1, LinkResource? resource2) => EqualityComparer<LinkResource>.Default.Equals(resource1, resource2);

        /// <summary>
        /// Determines whether the specified <see cref="object"/>, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object? obj) => Equals(obj as LinkResource);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref>
        /// parameter; otherwise, false.
        /// </returns>
        public bool Equals(LinkResource? other)
        {
            return other != null
                   && Source == other.Source;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data
        /// structures like a hash table.
        /// </returns>
        public override int GetHashCode() => 924162744 + EqualityComparer<string>.Default.GetHashCode(Source);

        /// <summary>
        /// Gets the content of the resource as an IHtmlContent item.
        /// </summary>
        /// <returns>The content.</returns>
        public IHtmlContent GetHtmlContent()
        {
            var Builder = new TagBuilder("link");
            var FinalSource = Source;
            if (!string.IsNullOrEmpty(Version))
                FinalSource += "?v=" + Version;
            MergeAttribute(Builder, "href", FinalSource);
            MergeAttribute(Builder, "rel", Rel);
            MergeAttribute(Builder, "type", Type);
            MergeAttribute(Builder, "charset", Charset);
            MergeAttribute(Builder, "crossorigin", CrossOrigin);
            MergeAttribute(Builder, "hreflang", HrefLang);
            MergeAttribute(Builder, "media", Media);
            MergeAttribute(Builder, "rev", Rev);
            MergeAttribute(Builder, "sizes", Sizes);
            MergeAttribute(Builder, "target", Target);
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
    }
}