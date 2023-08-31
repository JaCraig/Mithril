using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mithril.Themes.Abstractions.Interfaces;

namespace Mithril.Themes.Resources
{
    /// <summary>
    /// Script file resource.
    /// </summary>
    /// <seealso cref="IEquatable{ScriptFileResource}"/>
    /// <seealso cref="IResource"/>
    public class ScriptFileResource : IResource, IEquatable<ScriptFileResource>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptFileResource" /> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="async">The asynchronous.</param>
        /// <param name="charset">The charset.</param>
        /// <param name="defer">The defer.</param>
        /// <param name="type">The type.</param>
        /// <param name="xMLSpace">The xml space.</param>
        /// <param name="order">The order.</param>
        /// <param name="location">The location.</param>
        /// <param name="integrity">The integrity.</param>
        /// <param name="crossOrigin">The cross origin.</param>
        /// <param name="referrerPolicy">The referrer policy.</param>
        public ScriptFileResource(string? source, string? async, string? charset, string? defer, string? type, string? xMLSpace, int order, string? location, string? integrity, string? crossOrigin, string? referrerPolicy)
        {
            Source = source?.Replace("~/", "/") ?? "";
            Order = order;
            Integrity = integrity ?? "";
            CrossOrigin = crossOrigin ?? "";
            ReferrerPolicy = referrerPolicy ?? "";
            Async = async ?? "";
            Charset = charset ?? "";
            Defer = string.Equals(defer, "TRUE", StringComparison.OrdinalIgnoreCase);
            Type = type ?? "";
            XMLSpace = xMLSpace ?? "";
            Location = string.IsNullOrEmpty(location) ? "Footer" : location;
            Version = "";
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
        /// Gets the location.
        /// </summary>
        /// <value>The location.</value>
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
        /// Gets or sets the source.
        /// </summary>
        /// <value>The source.</value>
        public string Source { get; set; }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        public string Type { get; }

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>The version.</value>
        public string Version { get; }

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
        public static bool operator !=(ScriptFileResource? resource1, ScriptFileResource? resource2) => !(resource1 == resource2);

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="resource1">The resource1.</param>
        /// <param name="resource2">The resource2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(ScriptFileResource? resource1, ScriptFileResource? resource2) => EqualityComparer<ScriptFileResource>.Default.Equals(resource1, resource2);

        /// <summary>
        /// Determines whether the specified <see cref="object"/>, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object? obj) => Equals(obj as ScriptFileResource);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref>
        /// parameter; otherwise, false.
        /// </returns>
        public bool Equals(ScriptFileResource? other)
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
            var Builder = new TagBuilder("script");
            var FinalSource = Source;
            if (!string.IsNullOrEmpty(Version))
                FinalSource += "?v=" + Version;
            MergeAttribute(Builder, "src", FinalSource);
            MergeAttribute(Builder, "async", Async);
            MergeAttribute(Builder, "charset", Charset);
            MergeAttribute(Builder, "defer", Defer);
            MergeAttribute(Builder, "type", Type);
            MergeAttribute(Builder, "xml:space", XMLSpace);
            MergeAttribute(Builder, "integrity", Integrity);
            MergeAttribute(Builder, "crossorigin", CrossOrigin);
            MergeAttribute(Builder, "referrerpolicy", ReferrerPolicy);
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