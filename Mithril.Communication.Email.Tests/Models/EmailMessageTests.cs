using Mithril.Communication.Email.Models;
using Mithril.Tests.Helpers;

namespace Mithril.Communication.Email.Tests.Models
{
    /// <summary>
    /// Email message tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;EmailMessage&gt;"/>
    public class EmailMessageTests : TestBaseClass<EmailMessage>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailMessageTests"/> class.
        /// </summary>
        public EmailMessageTests()
        {
            TestObject = new EmailMessage();
        }
    }
}