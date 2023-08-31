using Mithril.Admin.Abstractions.DataEditor.Attributes;
using Mithril.API.Abstractions.Admin.DropDowns;
using Mithril.Data.Models.Contact;
using Mithril.Data.Models.General;

namespace Mithril.Security.Admin.ViewModels
{
    /// <summary>
    /// Contact Information VM
    /// </summary>
    public class ContactInformationVM
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContactInformationVM" /> class.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="contactInfoTypes">The contact information types.</param>
        public ContactInformationVM(ContactInfo? model, LookUpType? contactInfoTypes)
        {
            if (model is null || contactInfoTypes is null)
                return;
            Info = model.Info ?? "";
            ContactType = contactInfoTypes.LookUps.FirstOrDefault(x => x.DisplayName == model.Type)?.ID;
        }

        /// <summary>
        /// Gets or sets the type of the contact.
        /// </summary>
        /// <value>
        /// The type of the contact.
        /// </value>
        [Select(typeof(LookUpDropDown), "ContactInfo")]
        public long? ContactType { get; set; }

        /// <summary>
        /// Gets or sets the information.
        /// </summary>
        /// <value>
        /// The information.
        /// </value>
        public string? Info { get; set; }
    }
}