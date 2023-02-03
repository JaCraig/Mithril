using Mithril.Logging.Commands;
using Mithril.Tests.Helpers;

namespace Mithril.Logging.Tests.Commands
{
    /// <summary>
    /// LogCommandHandler tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;LogCommandHandler&gt;"/>
    public class LogCommandHandlerTests : TestBaseClass<LogCommandHandler>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogCommandHandlerTests"/> class.
        /// </summary>
        public LogCommandHandlerTests()
        {
            TestObject = new LogCommandHandler(null, null);
            ObjectType = typeof(LogCommandHandler);
        }
    }
}