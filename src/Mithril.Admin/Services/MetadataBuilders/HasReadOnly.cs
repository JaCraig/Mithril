﻿using Mithril.Admin.Abstractions.BaseClasses;
using Mithril.Admin.Abstractions.DataEditor;
using Mithril.Admin.Abstractions.DataEditor.Attributes;
using Mithril.Admin.Abstractions.ExtensionMethods;
using Mithril.Admin.Abstractions.Services;

namespace Mithril.Admin.Services.MetadataBuilders
{
    /// <summary>
    /// Determines if the property has a read only value.
    /// </summary>
    /// <seealso cref="MetadataBuilderBaseClass" />
    public class HasReadOnly : MetadataBuilderBaseClass
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
            if (!propertyMetadata.Property.HasAttribute<ReadOnlyAttribute>() && propertyMetadata.Property.GetSetMethod() is not null)
                return propertyMetadata;
            propertyMetadata.Metadata["readonly"] = true;
            return propertyMetadata;
        }
    }
}