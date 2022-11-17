using Mithril.Core.Abstractions.Modules.Features;
using Mithril.Tests.Helpers;

namespace Mithril.Core.Abstractions.Tests.Modules.Features
{
    public class EmailFeatureTests : TestBaseClass<EmailFeature>
    {
        public EmailFeatureTests()
        {
            TestObject = new EmailFeature();
        }
    }
}