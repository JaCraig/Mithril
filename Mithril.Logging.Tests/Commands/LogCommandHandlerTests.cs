using Mithril.Logging.Commands;
using Mithril.Tests.Helpers;

namespace Mithril.Logging.Tests.Commands
{
    public class LogCommandHandlerTests : TestBaseClass<LogCommandHandler>
    {
        public LogCommandHandlerTests()
        {
            TestObject = new LogCommandHandler(null, null);
        }
    }
}