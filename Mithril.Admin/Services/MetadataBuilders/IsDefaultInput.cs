using Mithril.Admin.Abstractions.BaseClasses;
using Mithril.Admin.Abstractions.DataEditor;

namespace Mithril.Admin.Services.MetadataBuilders
{
    /// <summary>
    /// Is Default Input type
    /// TODO: Add tests
    /// </summary>
    /// <seealso cref="MetadataBuilderBaseClass"/>
    public class IsDefaultInput : MetadataBuilderBaseClass
    {
        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        public override int Order => int.MaxValue;

        /// <summary>
        /// Extracts metadata and adds it to the PropertyMetadata object.
        /// </summary>
        /// <param name="propertyMetadata">The property metadata.</param>
        /// <returns>The resulting property metadata.</returns>
        public override PropertyMetadata? ExtractMetadata(PropertyMetadata? propertyMetadata)
        {
            if (propertyMetadata is null)
                return propertyMetadata;
            if (string.IsNullOrEmpty(propertyMetadata.PropertyType))
            {
                propertyMetadata.PropertyType = "input";
                propertyMetadata.Metadata["inputType"] = "text";
            }
            return propertyMetadata;
        }
    }
}