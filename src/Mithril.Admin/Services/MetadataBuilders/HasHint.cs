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
    /// Determines if the property has a hint.
    /// </summary>
    /// <seealso cref="MetadataBuilderBaseClass" />
    public class HasHint : MetadataBuilderBaseClass
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
            if (propertyMetadata?.Property.HasAttribute<HintAttribute>() != true)
                return propertyMetadata;
            propertyMetadata.Metadata["hint"] = DetermineHint(propertyMetadata.Property);
            return propertyMetadata;
        }

        /// <summary>
        /// Determines the hint.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>The hint text if one exists.</returns>
        private static string? DetermineHint(PropertyInfo? property)
        {
            HintAttribute? TempAttribute = property?.Attribute<HintAttribute>();
            return TempAttribute is null ? "" : TempAttribute.Value;
        }
    }
}