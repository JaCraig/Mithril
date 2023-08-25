using Mithril.Apm.Default.Admin.ViewModels;
using Mithril.Tests.Helpers;

namespace Mithril.Apm.Default.Tests.Admin.ViewModels
{
    /// <summary>
    /// Request trace VM tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;RequestTraceVM&gt;" />
    public class RequestTraceVMTests : TestBaseClass<RequestTraceVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestTraceVMTests"/> class.
        /// </summary>
        public RequestTraceVMTests()
        {
            TestObject = new RequestTraceVM();
            ObjectType = typeof(RequestTraceVM);
        }
    }
}