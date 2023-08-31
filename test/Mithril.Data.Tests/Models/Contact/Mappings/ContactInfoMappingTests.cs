using Mithril.Data.Models.Contact.Mappings;
using Mithril.Tests.Helpers;

namespace Mithril.Data.Tests.Models.Mappings
{
    /// <summary>
    /// Contact info mapping tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;ContactInfoMapping&gt;" />
    public class ContactInfoMappingTests : TestBaseClass<ContactInfoMapping>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContactInfoMappingTests"/> class.
        /// </summary>
        public ContactInfoMappingTests()
        {
            TestObject = new ContactInfoMapping();
        }
    }
}