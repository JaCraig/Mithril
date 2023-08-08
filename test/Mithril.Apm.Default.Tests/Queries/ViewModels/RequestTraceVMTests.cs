using Mithril.Apm.Default.Queries.ViewModels;
using Mithril.Tests.Helpers;

namespace Mithril.Apm.Default.Tests.Queries.ViewModels
{
    /// <summary>
    /// RequestTraceVM tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;RequestTraceVM&gt;"/>
    public class RequestTraceVMTests : TestBaseClass<RequestTraceVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestTraceVMTests"/> class.
        /// </summary>
        public RequestTraceVMTests()
        {
            TestObject = new RequestTraceVM(null);
            ObjectType = typeof(RequestTraceVM);
        }
    }
}