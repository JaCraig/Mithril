using Mithril.Apm.Default.Admin.ViewModels;
using Mithril.Tests.Helpers;

namespace Mithril.Apm.Default.Tests.Admin.ViewModels
{
    /// <summary>
    /// Request metric VM tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;RequestMetricVM&gt;" />
    public class RequestMetricVMTests : TestBaseClass<RequestMetricVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestMetricVMTests"/> class.
        /// </summary>
        public RequestMetricVMTests()
        {
            TestObject = new RequestMetricVM();
            ObjectType = typeof(RequestMetricVM);
        }
    }
}