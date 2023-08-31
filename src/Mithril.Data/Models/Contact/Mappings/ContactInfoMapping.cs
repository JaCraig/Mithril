using Inflatable.BaseClasses;
using Mithril.Data.Abstractions.Databases;

namespace Mithril.Data.Models.Contact.Mappings
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
            _ = Reference(x => x.Info).WithDefaultValue(() => "");
        }
    }
}