﻿using BigBook;
using BigBook.ExtensionMethods;
using Mithril.Core.Abstractions.Extensions;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Mithril.Admin.Abstractions.DataEditor
{
    /// <summary>
    /// Property metadata
    /// </summary>
    /// <remarks>Initializes a new instance of the <see cref="PropertyMetadata"/> class.</remarks>
    /// <param name="property">The property.</param>
    public class PropertyMetadata(PropertyInfo? property)
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [JsonPropertyName("displayName")]
        public string? DisplayName { get; set; } = property?.Name?.ToPascalCase()?.AddSpaces() ?? "";

        /// <summary>
        /// Gets or sets the metadata.
        /// </summary>
        /// <value>The metadata.</value>
        [JsonPropertyName("metadata")]
        public Dictionary<string, object?> Metadata { get; } = [];

        /// <summary>
        /// Gets or sets the property.
        /// </summary>
        /// <value>The property.</value>
        [JsonIgnore]
        public PropertyInfo? Property { get; set; } = property;

        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        /// <value>The name of the property.</value>
        [JsonPropertyName("propertyName")]
        public string? PropertyName { get; set; } = property?.Name?.ToString(StringCase.CamelCase) ?? "";

        /// <summary>
        /// Gets or sets the type of the property.
        /// </summary>
        /// <value>The type of the property.</value>
        [JsonPropertyName("propertyType")]
        public string? PropertyType { get; set; }
    }
}