using BigBook;
using Mithril.Admin.Abstractions.BaseClasses;
using Mithril.Admin.Abstractions.DataEditor;
using Mithril.Admin.Abstractions.Interfaces;
using Mithril.Admin.Abstractions.Services;
using System.ComponentModel.DataAnnotations;

namespace Mithril.Admin.Services.MetadataBuilders
{
    /// <summary>
    /// Extracts standard validation metadata
    /// TODO: Add tests.
    /// </summary>
    /// <seealso cref="IMetadataBuilder"/>
    public class StandardValidation : MetadataBuilderBaseClass
    {
        /// <summary>
        /// Extracts metadata and adds it to the PropertyMetadata object.
        /// </summary>
        /// <param name="propertyMetadata">The property metadata.</param>
        /// <returns>The resulting property metadata.</returns>
        public override PropertyMetadata? ExtractMetadata(PropertyMetadata? propertyMetadata, IEntityMetadataService metadataService)
        {
            if (propertyMetadata is null)
                return propertyMetadata;
            if (propertyMetadata.Property?.Attributes<RequiredAttribute>()?.FirstOrDefault() is not null)
                propertyMetadata.Metadata["required"] = true;

            MaxLengthAttribute? MaxLength = propertyMetadata.Property?.Attributes<MaxLengthAttribute>()?.FirstOrDefault();
            if (MaxLength is not null)
                propertyMetadata.Metadata["maxlength"] = MaxLength.Length;

            MinLengthAttribute? MinLength = propertyMetadata.Property?.Attributes<MinLengthAttribute>()?.FirstOrDefault();
            if (MinLength is not null)
                propertyMetadata.Metadata["minlength"] = MinLength.Length;

            RangeAttribute? RangeAttribute = propertyMetadata.Property?.Attributes<RangeAttribute>()?.FirstOrDefault();
            if (RangeAttribute?.Minimum is not null)
                propertyMetadata.Metadata["minlength"] = RangeAttribute.Minimum;
            if (RangeAttribute?.Maximum is not null)
                propertyMetadata.Metadata["maxlength"] = RangeAttribute.Maximum;

            StringLengthAttribute? StringLengthAttribute = propertyMetadata.Property?.Attributes<StringLengthAttribute>()?.FirstOrDefault();
            if (StringLengthAttribute?.MinimumLength > 0)
                propertyMetadata.Metadata["minlength"] = StringLengthAttribute.MinimumLength;
            if (StringLengthAttribute?.MaximumLength > 0)
                propertyMetadata.Metadata["maxlength"] = StringLengthAttribute.MaximumLength;

            AddValidationMessages(propertyMetadata);

            return propertyMetadata;
        }

        /// <summary>
        /// Adds the validation messages.
        /// </summary>
        /// <param name="propertyMetadata">The property metadata.</param>
        private static void AddValidationMessages(PropertyMetadata propertyMetadata)
        {
            if (propertyMetadata.Metadata.TryGetValue("maxlength", out var MaxLength))
                propertyMetadata.Metadata["errorMessageTooLong"] = $"{propertyMetadata.DisplayName} can only have a maximum length of {MaxLength}.";
            if (propertyMetadata.Metadata.TryGetValue("minlength", out var MinLength))
                propertyMetadata.Metadata["errorMessageTooShort"] = $"{propertyMetadata.DisplayName} must have a minimum length of {MinLength}.";
            if (propertyMetadata.Metadata.TryGetValue("required", out _))
                propertyMetadata.Metadata["errorMessageValueMissing"] = $"{propertyMetadata.DisplayName} is required.";
        }
    }
}