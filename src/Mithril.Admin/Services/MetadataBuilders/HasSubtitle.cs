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
    /// Determines if the property has a subtitle value.
    /// </summary>
    /// <seealso cref="MetadataBuilderBaseClass" />
    public class HasSubtitle : MetadataBuilderBaseClass
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
            if (propertyMetadata?.Property.HasAttribute<SubtitleAttribute>() != true)
                return propertyMetadata;
            propertyMetadata.Metadata["subtitle"] = DetermineSubtitle(propertyMetadata.Property);
            return propertyMetadata;
        }

        /// <summary>
        /// Determines the Subtitle.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>The Subtitle value if one exists.</returns>
        protected static string? DetermineSubtitle(PropertyInfo? property)
        {
            SubtitleAttribute? TempAttribute = property?.Attribute<SubtitleAttribute>();
            return TempAttribute is null ? "" : TempAttribute.Value;
        }
    }
}