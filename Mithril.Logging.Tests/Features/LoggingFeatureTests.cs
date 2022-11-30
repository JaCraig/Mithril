using Mithril.Logging.Features;
using Mithril.Tests.Helpers;

namespace Mithril.Logging.Tests.Features
{
    public class LoggingFeatureTests : TestBaseClass<LoggingFeature>
    {
        public LoggingFeatureTests()
        {
            TestObject = new LoggingFeature();
        }
    }
}