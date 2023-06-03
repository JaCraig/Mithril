using Mithril.Admin.Abstractions.BaseClasses;
using Mithril.Admin.Abstractions.DataEditor;
using Mithril.Admin.Abstractions.DataEditor.Attributes;
using Mithril.Admin.Abstractions.Services;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Mithril.Admin.Services.MetadataBuilders
{
    /// <summary>
    /// Can list
    /// TODO: Add tests
    /// </summary>
    /// <seealso cref="MetadataBuilderBaseClass" />
    public class CanList : MetadataBuilderBaseClass
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
            if (propertyMetadata is null)
                return propertyMetadata;
            propertyMetadata.Metadata["canList"] = false;
            var propertyType = propertyMetadata.Property?.PropertyType;
            if (propertyType is null)
                return propertyMetadata;
            propertyMetadata.Metadata["canList"] = propertyMetadata.Property?.GetCustomAttribute<JsonIgnoreAttribute>() is null
                    && propertyMetadata.Property?.GetCustomAttribute<DoNotListAttribute>() is null
                    && (propertyType.IsPrimitive || propertyType.IsEnum || propertyType == typeof(string) || propertyType == typeof(DateTime));
            return propertyMetadata;
        }
    }
}