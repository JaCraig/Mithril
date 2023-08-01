using BigBook;
using Mithril.Admin.Abstractions.BaseClasses;
using Mithril.Admin.Abstractions.DataEditor;
using Mithril.Admin.Abstractions.Services;
using System.Collections;

namespace Mithril.Admin.Services.MetadataBuilders
{
    /// <summary>
    /// Determines if the property is a list.
    /// </summary>
    /// <seealso cref="MetadataBuilderBaseClass" />
    public class IsList : MetadataBuilderBaseClass
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
            if (propertyMetadata?.Property is null || propertyMetadata.Property.PropertyType == typeof(string))
                return propertyMetadata;
            if (propertyMetadata.Property.PropertyType.IsAssignableTo(typeof(IEnumerable))
                && propertyMetadata.Property.PropertyType.GetIEnumerableElementType().IsClass)
            {
                propertyMetadata.PropertyType = "complex-list";
                propertyMetadata.Metadata.Add("fields", metadataService.ExtractMetadata(propertyMetadata.Property)?.Properties);
            }

            return propertyMetadata;
        }
    }
}