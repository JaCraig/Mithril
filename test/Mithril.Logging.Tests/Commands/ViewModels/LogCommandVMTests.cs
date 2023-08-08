using Mithril.Logging.Commands.ViewModels;
using Mithril.Tests.Helpers;

namespace Mithril.Logging.Tests.Commands.ViewModels
{
    /// <summary>
    /// Log Command VM Tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;LogCommandVM&gt;"/>
    public class LogCommandVMTests : TestBaseClass<LogCommandVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogCommandVMTests"/> class.
        /// </summary>
        public LogCommandVMTests()
        {
            TestObject = new LogCommandVM();
        }
    }
}