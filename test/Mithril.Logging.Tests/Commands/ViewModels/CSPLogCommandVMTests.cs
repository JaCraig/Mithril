using Mithril.Logging.Commands.ViewModels;
using Mithril.Tests.Helpers;

namespace Mithril.Logging.Tests.Commands.ViewModels
{
    /// <summary>
    /// CSP Log command VM tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;CSPLogCommandVM&gt;"/>
    public class CSPLogCommandVMTests : TestBaseClass<CSPLogCommandVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSPLogCommandVMTests"/> class.
        /// </summary>
        public CSPLogCommandVMTests()
        {
            TestObject = new CSPLogCommandVM();
            ObjectType = typeof(CSPLogCommandVM);
        }
    }
}