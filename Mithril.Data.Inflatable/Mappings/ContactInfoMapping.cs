using Inflatable.BaseClasses;
using Mithril.Core.Abstractions.Data.Models;
using Mithril.Data.Inflatable.Databases;

namespace Mithril.Core.Models.Mappings
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