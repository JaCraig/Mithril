﻿using Mithril.Admin.Abstractions.DataEditor;
using Mithril.Admin.Abstractions.Services;

namespace Mithril.Admin.Abstractions.Interfaces
{
    /// <summary>
    /// Metadata builder interface
    /// </summary>
    public interface IMetadataBuilder
    {
        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        int Order { get; }

        /// <summary>
        /// Extracts metadata and adds it to the PropertyMetadata object.
        /// </summary>
        /// <param name="propertyMetadata">The property metadata.</param>
        /// <param name="metadataService">The metadata service.</param>
        /// <returns>
        /// The resulting property metadata.
        /// </returns>
        PropertyMetadata? ExtractMetadata(PropertyMetadata? propertyMetadata, IEntityMetadataService metadataService);
    }
}