using Mithril.Logging.Serilog.Middleware;
using Mithril.Tests.Helpers;

namespace Mithril.Logging.Serilog.Tests.Middleware
{
    public class LoggingMiddlewareTests : TestBaseClass<LoggingMiddleware>
    {
        public LoggingMiddlewareTests()
        {
            TestObject = new LoggingMiddleware(null);
        }
    }
}