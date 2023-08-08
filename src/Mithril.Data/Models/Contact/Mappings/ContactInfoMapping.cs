using Inflatable.BaseClasses;
using Mithril.Data.Abstractions.Databases;
using Mithril.Data.Models.Contact;

namespace Mithril.Data.Inflatable.Models.Contact.Mappings
{
    /// <summary>
    /// ContactInfo mapping
    /// </summary>
    /// <seealso cref="MappingBaseClass&lt;ContactInfo, DefaultDatabase&gt;"/>
    public class ContactInfoMapping : MappingBaseClass<ContactInfo, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContactInfoMapping"/> class.
        /// </summary>
        public ContactInfoMapping()
            : base(merge: true)
        {
            Reference(x => x.Info).WithDefaultValue(() => "");
        }
    }
}