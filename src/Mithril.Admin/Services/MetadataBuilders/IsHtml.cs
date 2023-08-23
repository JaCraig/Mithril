using BigBook;
using Mithril.Admin.Abstractions.BaseClasses;
using Mithril.Admin.Abstractions.DataEditor;
using Mithril.Admin.Abstractions.DataEditor.Attributes;
using Mithril.Admin.Abstractions.Services;

namespace Mithril.Admin.Services.MetadataBuilders
{
    /// <summary>
    /// Is HTML
    /// TODO: Add tests
    /// </summary>
    /// <seealso cref="MetadataBuilderBaseClass" />
    public class IsHtml : MetadataBuilderBaseClass
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
            if (propertyMetadata?.Property is null)
                return propertyMetadata;
            if (propertyMetadata.Property.Attribute<HtmlAttribute>() is not null)
            {
                propertyMetadata.PropertyType = "html";
                return propertyMetadata;
            }

            return propertyMetadata;
        }
    }
}