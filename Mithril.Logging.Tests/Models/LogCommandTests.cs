using Mithril.Logging.Models;
using Mithril.Tests.Helpers;

namespace Mithril.Logging.Tests.Models
{
    public class LogCommandTests : TestBaseClass<LogCommand>
    {
        public LogCommandTests()
        {
            TestObject = new LogCommand();
            ObjectType = typeof(LogCommand);
        }
    }
}