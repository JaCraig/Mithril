using Mithril.Data.Models.Contact;
using Mithril.Tests.Helpers;

namespace Mithril.Data.Tests.Models
{
    /// <summary>
    /// Contact info tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;ContactInfo&gt;" />
    public class ContactInfoTests : TestBaseClass<ContactInfo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContactInfoTests"/> class.
        /// </summary>
        public ContactInfoTests()
        {
            TestObject = new ContactInfo();
            ObjectType = typeof(ContactInfo);
        }
    }
}