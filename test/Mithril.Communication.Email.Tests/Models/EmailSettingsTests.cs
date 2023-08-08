using Mithril.Communication.Email.Models;
using Mithril.Tests.Helpers;

namespace Mithril.Communication.Email.Tests.Models
{
    /// <summary>
    /// Email Settings Tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;EmailSettings&gt;"/>
    public class EmailSettingsTests : TestBaseClass<EmailSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailSettingsTests"/> class.
        /// </summary>
        public EmailSettingsTests()
        {
            TestObject = new EmailSettings();
        }
    }
}