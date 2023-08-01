using Mithril.Admin.Abstractions.BaseClasses;
using Mithril.Admin.Abstractions.DataEditor;
using Mithril.Admin.Abstractions.ExtensionMethods;
using Mithril.Admin.Abstractions.Services;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Mithril.Admin.Services.MetadataBuilders
{
    /// <summary>
    /// Determines if the property has a Min value.
    /// </summary>
    /// <seealso cref="MetadataBuilderBaseClass" />
    public class HasMin : MetadataBuilderBaseClass
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
            if (propertyMetadata?.Property.HasAttribute<RangeAttribute>() != true)
                return propertyMetadata;
            propertyMetadata.Metadata["min"] = DetermineMinValue(propertyMetadata.Property);
            return propertyMetadata;
        }

        /// <summary>
        /// Determines the Minimum value.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>The Min value.</returns>
        private static decimal DetermineMinValue(PropertyInfo? property)
        {
            var MinValueAttribute = property?.GetCustomAttribute<RangeAttribute>();
            if (MinValueAttribute is null)
                return 100;
            return (int)(MinValueAttribute.Minimum ?? 100);
        }
    }
}