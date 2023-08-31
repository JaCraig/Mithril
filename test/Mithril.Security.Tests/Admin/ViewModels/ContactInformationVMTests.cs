using Mithril.Security.Admin.ViewModels;
using Mithril.Tests.Helpers;

namespace Mithril.Security.Tests.Admin.ViewModels
{
    /// <summary>
    /// Contact Information VM Tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;ContactInformationVM&gt;" />
    public class ContactInformationVMTests : TestBaseClass<ContactInformationVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContactInformationVMTests"/> class.
        /// </summary>
        public ContactInformationVMTests()
        {
            TestObject = new ContactInformationVM(null, null);
            ObjectType = typeof(ContactInformationVM);
        }
    }
}