﻿using Mithril.Admin.Abstractions.BaseClasses;
using Mithril.Admin.Abstractions.DataEditor;
using Mithril.Admin.Abstractions.ExtensionMethods;
using Mithril.Admin.Abstractions.Services;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Mithril.Admin.Services.MetadataBuilders
{
    /// <summary>
    /// Determines if the property has a max value.
    /// </summary>
    /// <seealso cref="MetadataBuilderBaseClass" />
    public class HasMax : MetadataBuilderBaseClass
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
            propertyMetadata.Metadata["max"] = DetermineMaxValue(propertyMetadata.Property);
            return propertyMetadata;
        }

        /// <summary>
        /// Determines the maximum value.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>The max value.</returns>
        private static decimal DetermineMaxValue(PropertyInfo? property)
        {
            RangeAttribute? MaxValueAttribute = property?.GetCustomAttribute<RangeAttribute>();
            return MaxValueAttribute is null ? 100 : (int)(MaxValueAttribute.Maximum ?? 100);
        }
    }
}