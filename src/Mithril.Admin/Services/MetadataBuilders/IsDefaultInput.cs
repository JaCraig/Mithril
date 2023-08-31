using BigBook;
using Mithril.Admin.Abstractions.BaseClasses;
using Mithril.Admin.Abstractions.DataEditor;
using Mithril.Admin.Abstractions.DataEditor.Attributes;
using Mithril.Admin.Abstractions.Services;
using System.Reflection;

namespace Mithril.Admin.Services.MetadataBuilders
{
    /// <summary>
    /// Is Default Input type
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
        /// <param name="metadataService">The metadata service.</param>
        /// <returns>
        /// The resulting property metadata.
        /// </returns>
        public override PropertyMetadata? ExtractMetadata(PropertyMetadata? propertyMetadata, IEntityMetadataService metadataService)
        {
            if (propertyMetadata is null || !string.IsNullOrEmpty(propertyMetadata.PropertyType))
                return propertyMetadata;
            propertyMetadata.PropertyType = "input";
            propertyMetadata.Metadata["inputType"] = GetPropertyType(propertyMetadata.Property);
            propertyMetadata.Metadata["isUTC"] = true;
            return propertyMetadata;
        }

        /// <summary>
        /// Gets the type of the property.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>The input type.</returns>
        private static string GetPropertyType(PropertyInfo? property)
        {
            if (property is null)
                return "text";
            InputTypeAttribute? DeclaredType = property.Attribute<InputTypeAttribute>();
            if (!string.IsNullOrEmpty(DeclaredType?.InputType))
                return DeclaredType.InputType;
            if (property.PropertyType.Is<uint>())
                return "number";
            if (property.PropertyType.Is<ulong>())
                return "number";
            if (property.PropertyType.Is<ushort>())
                return "number";
            if (property.PropertyType.Is<int>())
                return "number";
            if (property.PropertyType.Is<long>())
                return "number";
            return property.PropertyType.Is<short>()
                ? "number"
                : property.Attribute<PasswordAttribute>() is not null
                ? "password"
                : property.Attribute<DateAndTimeAttribute>() is not null
                ? "datetime-local"
                : property.PropertyType.Is<DateTime>() ? "date" : "text";
        }
    }
}