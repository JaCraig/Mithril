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
    /// Determines if the property has a step value.
    /// </summary>
    /// <seealso cref="MetadataBuilderBaseClass" />
    public class HasStep : MetadataBuilderBaseClass
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
            if (propertyMetadata?.Property.HasAttribute<StepAttribute>() != true)
                return propertyMetadata;
            propertyMetadata.Metadata["step"] = Determinestep(propertyMetadata.Property);
            return propertyMetadata;
        }

        /// <summary>
        /// Determines the step.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>The step value if one exists.</returns>
        protected static decimal? Determinestep(PropertyInfo? property)
        {
            var TempAttribute = property?.Attribute<StepAttribute>();
            if (TempAttribute is null)
                return 1;
            return TempAttribute.Value;
        }
    }
}