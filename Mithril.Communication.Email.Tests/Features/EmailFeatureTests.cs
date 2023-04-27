using Mithril.Communication.Email.Features;
using Mithril.Tests.Helpers;

namespace Mithril.Communication.Email.Tests.Features
{
    /// <summary>
    /// Email feature tests
    /// </summary>
    /// <seealso cref="Mithril.Tests.Helpers.TestBaseClass&lt;Mithril.Communication.Email.Features.EmailFeature&gt;" />
    public class EmailFeatureTests : TestBaseClass<EmailFeature>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailFeatureTests"/> class.
        /// </summary>
        public EmailFeatureTests()
        {
            TestObject = new EmailFeature();
        }
    }
}