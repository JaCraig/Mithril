﻿using BigBook;
using Mithril.Admin.Abstractions.BaseClasses;
using Mithril.Admin.Abstractions.DataEditor;
using Mithril.Admin.Abstractions.DataEditor.Attributes;
using Mithril.Admin.Abstractions.ExtensionMethods;
using Mithril.Admin.Abstractions.Services;

namespace Mithril.Admin.Services.MetadataBuilders
{
    /// <summary>
    /// Determines if the property is a TextArea object.
    /// </summary>
    /// <seealso cref="MetadataBuilderBaseClass" />
    public class IsTextArea : MetadataBuilderBaseClass
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
            if (propertyMetadata?.Property.HasAttribute<TextAreaAttribute>() != true)
                return propertyMetadata;
            propertyMetadata.PropertyType = "textarea";
            propertyMetadata.Metadata["rows"] = propertyMetadata.Property?.Attribute<TextAreaAttribute>()?.Rows ?? 3;
            return propertyMetadata;
        }
    }
}