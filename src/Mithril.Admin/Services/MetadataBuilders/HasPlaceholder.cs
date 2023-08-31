using BigBook;
using Mithril.Admin.Abstractions.BaseClasses;
using Mithril.Admin.Abstractions.DataEditor;
using Mithril.Admin.Abstractions.DataEditor.Attributes;
using Mithril.Admin.Abstractions.ExtensionMethods;
using Mithril.Admin.Abstractions.Services;
using System.Reflection;

namespace Mithril.Admin.Services.MetadataBuilders
{
    /// <summary>
    /// Determines if the property has a placeholder value.
    /// </summary>
    /// <seealso cref="MetadataBuilderBaseClass" />
    public class HasPlaceholder : MetadataBuilderBaseClass
    {
        /// <summary>
        /// Extracts metadata and adds it to the PropertyMetadata object.
        /// </summary>
        /// <param name="propertyMetadata">The property metadata.</param>
        /// <param name="metadataService">The metadata service.</param>
        /// <returns>
        /// The resulting property metadata.
        /// </returns>
        public override PropertyMetadata? ExtractMetadata(PropertyMetadata? propertyMetadata, IEntityMetadataService metadataService)
        {
            if (propertyMetadata?.Property.HasAttribute<PlaceholderAttribute>() != true)
                return propertyMetadata;
            propertyMetadata.Metadata["placeholder"] = DeterminePlaceholder(propertyMetadata.Property);
            return propertyMetadata;
        }

        /// <summary>
        /// Determines the Placeholder.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>The Placeholder value if one exists.</returns>
        protected static string? DeterminePlaceholder(PropertyInfo? property)
        {
            PlaceholderAttribute? TempAttribute = property?.Attribute<PlaceholderAttribute>();
            return TempAttribute is null ? "" : TempAttribute.Value;
        }
    }
}