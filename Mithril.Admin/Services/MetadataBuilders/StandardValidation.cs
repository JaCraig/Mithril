using BigBook;
using Mithril.Admin.Abstractions.BaseClasses;
using Mithril.Admin.Abstractions.DataEditor;
using Mithril.Admin.Abstractions.Interfaces;
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
        public override PropertyMetadata? ExtractMetadata(PropertyMetadata? propertyMetadata)
        {
            if (propertyMetadata is null)
                return propertyMetadata;
            if (propertyMetadata.Property?.Attributes<RequiredAttribute>()?.FirstOrDefault() is not null)
                propertyMetadata.Metadata["required"] = true;

            MaxLengthAttribute? MaxLength = propertyMetadata.Property?.Attributes<MaxLengthAttribute>()?.FirstOrDefault();
            if (MaxLength is not null)
                propertyMetadata.Metadata["maxLength"] = MaxLength.Length;

            MinLengthAttribute? MinLength = propertyMetadata.Property?.Attributes<MinLengthAttribute>()?.FirstOrDefault();
            if (MinLength is not null)
                propertyMetadata.Metadata["minLength"] = MinLength.Length;

            RangeAttribute? RangeAttribute = propertyMetadata.Property?.Attributes<RangeAttribute>()?.FirstOrDefault();
            if (RangeAttribute is not null && RangeAttribute.Minimum is not null)
                propertyMetadata.Metadata["minLength"] = RangeAttribute.Minimum;
            if (RangeAttribute is not null && RangeAttribute.Maximum is not null)
                propertyMetadata.Metadata["maxLength"] = RangeAttribute.Maximum;

            StringLengthAttribute? StringLengthAttribute = propertyMetadata.Property?.Attributes<StringLengthAttribute>()?.FirstOrDefault();
            if (StringLengthAttribute is not null && StringLengthAttribute.MinimumLength > 0)
                propertyMetadata.Metadata["minLength"] = StringLengthAttribute.MinimumLength;
            if (StringLengthAttribute is not null && StringLengthAttribute.MaximumLength > 0)
                propertyMetadata.Metadata["maxLength"] = StringLengthAttribute.MaximumLength;

            AddValidationMessages(propertyMetadata);

            return propertyMetadata;
        }

        /// <summary>
        /// Adds the validation messages.
        /// </summary>
        /// <param name="propertyMetadata">The property metadata.</param>
        private void AddValidationMessages(PropertyMetadata propertyMetadata)
        {
            if (propertyMetadata.Metadata.TryGetValue("maxLength", out var MaxLength))
                propertyMetadata.Metadata["data-error-message-too-long"] = $"{propertyMetadata.DisplayName} is too long. Max length allowed is {MaxLength}.";
            if (propertyMetadata.Metadata.TryGetValue("minLength", out var MinLength))
                propertyMetadata.Metadata["data-error-message-too-short"] = $"{propertyMetadata.DisplayName} is too short. Min length allowed is {MinLength}.";
            if (propertyMetadata.Metadata.TryGetValue("required", out var Required))
                propertyMetadata.Metadata["data-error-message-value-missing"] = $"{propertyMetadata.DisplayName} is required.";
        }
    }
}