using Mithril.Communication.Email.Models.Mappings;
using Mithril.Tests.Helpers;

namespace Mithril.Communication.Email.Tests.Models.Mappings
{
    /// <summary>
    /// EmailSettingsMapping tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;EmailSettingsMapping&gt;"/>
    public class EmailSettingsMappingTests : TestBaseClass<EmailSettingsMapping>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailSettingsMappingTests"/> class.
        /// </summary>
        public EmailSettingsMappingTests()
        {
            TestObject = new EmailSettingsMapping();
        }
    }
}