using Mithril.Logging.Commands;
using Mithril.Tests.Helpers;

namespace Mithril.Logging.Tests.Commands
{
    /// <summary>
    /// CSP Log command handler tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;CSPLogCommandHandler&gt;"/>
    public class CSPLogCommandHandlerTests : TestBaseClass<CSPLogCommandHandler>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSPLogCommandHandlerTests"/> class.
        /// </summary>
        public CSPLogCommandHandlerTests()
        {
            TestObject = new CSPLogCommandHandler(null, null);
        }
    }
}