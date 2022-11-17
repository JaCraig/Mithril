using Mithril.Core.Abstractions.Modules.Features;
using Mithril.Tests.Helpers;

namespace Mithril.Core.Abstractions.Tests.Modules.Features
{
    public class LoggingFeatureTests : TestBaseClass<LoggingFeature>
    {
        public LoggingFeatureTests()
        {
            TestObject = new LoggingFeature();
        }
    }
}