using Mithril.Communication.Email.Channel;
using Mithril.Tests.Helpers;

namespace Mithril.Communication.Email.Tests.Channel
{
    /// <summary>
    /// Email channel tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;EmailChannel&gt;"/>
    public class EmailChannelTests : TestBaseClass<EmailChannel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailChannelTests"/> class.
        /// </summary>
        public EmailChannelTests()
        {
            TestObject = new EmailChannel(null, null, null, null);
            ObjectType = typeof(EmailChannel);
        }
    }
}