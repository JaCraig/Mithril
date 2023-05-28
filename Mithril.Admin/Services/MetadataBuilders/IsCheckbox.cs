using Mithril.Admin.Abstractions.BaseClasses;
using Mithril.Admin.Abstractions.DataEditor;

namespace Mithril.Admin.Services.MetadataBuilders
{
    /// <summary>
    /// Is Checkbox
    /// TODO: Add Tests
    /// </summary>
    /// <seealso cref="MetadataBuilderBaseClass"/>
    public class IsCheckbox : MetadataBuilderBaseClass
    {
        /// <summary>
        /// Extracts metadata and adds it to the PropertyMetadata object.
        /// </summary>
        /// <param name="propertyMetadata">The property metadata.</param>
        /// <returns>The resulting property metadata.</returns>
        public override PropertyMetadata? ExtractMetadata(PropertyMetadata? propertyMetadata)
        {
            if (propertyMetadata is null)
                return propertyMetadata;
            if (propertyMetadata.Property?.PropertyType.IsAssignableTo(typeof(bool)) ?? false)
                propertyMetadata.PropertyType = "checkbox";
            return propertyMetadata;
        }
    }
}