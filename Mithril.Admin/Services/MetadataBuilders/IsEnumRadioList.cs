using BigBook;
using Mithril.Admin.Abstractions.BaseClasses;
using Mithril.Admin.Abstractions.DataEditor;
using Mithril.Admin.Abstractions.Services;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Mithril.Admin.Services.MetadataBuilders
{
    /// <summary>
    /// Determines if this should be a radio list
    /// </summary>
    /// <seealso cref="MetadataBuilderBaseClass" />
    public class IsEnumRadioList : MetadataBuilderBaseClass
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
            if (propertyMetadata?.Property?.PropertyType.GetIEnumerableElementType().IsEnum != true)
                return propertyMetadata;
            propertyMetadata.PropertyType = "radio";
            propertyMetadata.Metadata["options"] = GenerateOptions(propertyMetadata.Property);
            return propertyMetadata;
        }

        /// <summary>
        /// Generates the options.
        /// </summary>
        /// <param name="_">The .</param>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        private static List<Option> GenerateOptions(PropertyInfo? property)
        {
            if (property is null)
                return new List<Option>();
            var ReturnValues = new List<Option>();
            foreach (var Item in property.PropertyType.GetEnumValues())
            {
                if (Item is null)
                    continue;
                var EnumName = Enum.GetName(property.PropertyType, Item);

                ReturnValues.AddIfUnique(new Option { Key = (int)Item, Value = EnumName?.AddSpaces() ?? "" });
            }
            return ReturnValues;
        }

        /// <summary>
        /// Option
        /// </summary>
        private class Option
        {
            /// <summary>
            /// Gets or sets the key.
            /// </summary>
            /// <value>
            /// The key.
            /// </value>
            [JsonPropertyName("key")]
            public int? Key { get; set; }

            /// <summary>
            /// Gets or sets the value.
            /// </summary>
            /// <value>
            /// The value.
            /// </value>
            [JsonPropertyName("value")]
            public string? Value { get; set; }
        }
    }
}