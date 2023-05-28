using BigBook;
using Mithril.Core.Abstractions.Extensions;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Mithril.Admin.Abstractions.DataEditor
{
    /// <summary>
    /// Property metadata
    /// TODO: Add tests
    /// </summary>
    public class PropertyMetadata
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyMetadata"/> class.
        /// </summary>
        /// <param name="property">The property.</param>
        public PropertyMetadata(PropertyInfo? property)
        {
            DisplayName = property?.Name?.ToPascalCase()?.AddSpaces() ?? "";
            PropertyName = property?.Name?.ToString(StringCase.CamelCase) ?? "";
            Property = property;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the metadata.
        /// </summary>
        /// <value>The metadata.</value>
        [JsonPropertyName("metadata")]
        public Dictionary<string, object?> Metadata { get; } = new Dictionary<string, object?>();

        /// <summary>
        /// Gets or sets the property.
        /// </summary>
        /// <value>The property.</value>
        [JsonIgnore]
        public PropertyInfo? Property { get; set; }

        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        /// <value>The name of the property.</value>
        [JsonPropertyName("propertyName")]
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the type of the property.
        /// </summary>
        /// <value>The type of the property.</value>
        [JsonPropertyName("propertyType")]
        public string PropertyType { get; set; }
    }
}