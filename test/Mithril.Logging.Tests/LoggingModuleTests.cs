using Mithril.Tests.Helpers;

namespace Mithril.Logging.Tests
{
    public class LoggingModuleTests : TestBaseClass<LoggingModule>
    {
        public LoggingModuleTests()
        {
            TestObject = new LoggingModule();
            DiscoverInheritedMethods = true;
        }
    }
}