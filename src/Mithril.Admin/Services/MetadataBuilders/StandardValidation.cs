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
    /// </summary>
    /// <seealso cref="IMetadataBuilder"/>
    public class StandardValidation : MetadataBuilderBaseClass
    {
        /// <summary>
        /// Extracts metadata and adds it to the PropertyMetadata object.
        /// </summary>
        /// <param name="propertyMetadata">The property metadata.</param>
        /// <param name="metadataService">The metadata service.</param>
        /// <returns>
        /// The resulting property metadata.
        /// </returns>
        public override PropertyMetadata? ExtractMetadata(PropertyMetadata? propertyMetadata, IEntityMetadataService? metadataService)
        {
            if (propertyMetadata is null)
                return propertyMetadata;
            CheckRequiredAttribute(propertyMetadata);

            CheckMaxLengthAttribute(propertyMetadata);

            CheckMinLengthAttribute(propertyMetadata);

            CheckRangeAttribute(propertyMetadata);

            CheckStringLengthAttribute(propertyMetadata);

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

        /// <summary>
        /// Checks the maximum length attribute.
        /// </summary>
        /// <param name="propertyMetadata">The property metadata.</param>
        private static void CheckMaxLengthAttribute(PropertyMetadata? propertyMetadata)
        {
            if (propertyMetadata is null)
                return;
            MaxLengthAttribute? MaxLength = propertyMetadata.Property?.Attributes<MaxLengthAttribute>()?.FirstOrDefault();
            if (MaxLength is null)
                return;
            propertyMetadata.Metadata["maxlength"] = MaxLength.Length;
        }

        /// <summary>
        /// Checks the minimum length attribute.
        /// </summary>
        /// <param name="propertyMetadata">The property metadata.</param>
        private static void CheckMinLengthAttribute(PropertyMetadata? propertyMetadata)
        {
            if (propertyMetadata is null)
                return;
            MinLengthAttribute? MinLength = propertyMetadata.Property?.Attributes<MinLengthAttribute>()?.FirstOrDefault();
            if (MinLength is null)
                return;
            propertyMetadata.Metadata["minlength"] = MinLength.Length;
        }

        /// <summary>
        /// Checks the range attribute.
        /// </summary>
        /// <param name="propertyMetadata">The property metadata.</param>
        private static void CheckRangeAttribute(PropertyMetadata? propertyMetadata)
        {
            if (propertyMetadata is null)
                return;
            RangeAttribute? RangeAttribute = propertyMetadata.Property?.Attributes<RangeAttribute>()?.FirstOrDefault();
            if (RangeAttribute is null)
                return;
            if (RangeAttribute.Minimum is not null)
                propertyMetadata.Metadata["minlength"] = RangeAttribute.Minimum;
            if (RangeAttribute.Maximum is not null)
                propertyMetadata.Metadata["maxlength"] = RangeAttribute.Maximum;
        }

        /// <summary>
        /// Checks the required attribute.
        /// </summary>
        /// <param name="propertyMetadata">The property metadata.</param>
        private static void CheckRequiredAttribute(PropertyMetadata? propertyMetadata)
        {
            if (propertyMetadata is null || propertyMetadata.Property?.Attributes<RequiredAttribute>()?.FirstOrDefault() is null)
                return;
            propertyMetadata.Metadata["required"] = true;
        }

        /// <summary>
        /// Checks the string length attribute.
        /// </summary>
        /// <param name="propertyMetadata">The property metadata.</param>
        private static void CheckStringLengthAttribute(PropertyMetadata? propertyMetadata)
        {
            if (propertyMetadata is null)
                return;
            StringLengthAttribute? StringLengthAttribute = propertyMetadata.Property?.Attributes<StringLengthAttribute>()?.FirstOrDefault();
            if (StringLengthAttribute is null)
                return;
            if (StringLengthAttribute.MinimumLength > 0)
                propertyMetadata.Metadata["minlength"] = StringLengthAttribute.MinimumLength;
            if (StringLengthAttribute.MaximumLength > 0)
                propertyMetadata.Metadata["maxlength"] = StringLengthAttribute.MaximumLength;
        }
    }
}