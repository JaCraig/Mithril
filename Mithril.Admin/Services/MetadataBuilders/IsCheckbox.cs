using Mithril.Admin.Abstractions.BaseClasses;
using Mithril.Admin.Abstractions.DataEditor;
using Mithril.Admin.Abstractions.Services;

namespace Mithril.Admin.Services.MetadataBuilders
{
    /// <summary>
    /// Is Checkbox
    /// </summary>
    /// <seealso cref="MetadataBuilderBaseClass"/>
    public class IsCheckbox : MetadataBuilderBaseClass
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
            if (propertyMetadata?.Property?.PropertyType != typeof(bool))
                return propertyMetadata;
            propertyMetadata.PropertyType = "checkbox";
            return propertyMetadata;
        }
    }
}