using Mithril.Communication.Email.Models.Mappings;
using Mithril.Tests.Helpers;

namespace Mithril.Communication.Email.Tests.Models.Mappings
{
    /// <summary>
    /// EmailMessageMapping tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;EmailMessageMapping&gt;"/>
    public class EmailMessageMappingTests : TestBaseClass<EmailMessageMapping>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailMessageMappingTests"/> class.
        /// </summary>
        public EmailMessageMappingTests()
        {
            TestObject = new EmailMessageMapping();
        }
    }
}