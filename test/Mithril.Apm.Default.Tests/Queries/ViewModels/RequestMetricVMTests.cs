using Mithril.Apm.Default.Queries.ViewModels;
using Mithril.Tests.Helpers;

namespace Mithril.Apm.Default.Tests.Queries.ViewModels
{
    /// <summary>
    /// RequestMetricVM tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;RequestMetricVM&gt;"/>
    public class RequestMetricVMTests : TestBaseClass<RequestMetricVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestMetricVMTests"/> class.
        /// </summary>
        public RequestMetricVMTests()
        {
            TestObject = new RequestMetricVM(null);
            ObjectType = typeof(RequestMetricVM);
        }
    }
}