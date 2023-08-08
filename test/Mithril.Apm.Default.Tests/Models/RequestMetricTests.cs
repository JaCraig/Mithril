using Mithril.Apm.Default.Models;
using Mithril.Tests.Helpers;

namespace Mithril.Apm.Default.Tests.Models
{
    /// <summary>
    /// Request metrics tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;RequestMetric&gt;"/>
    public class RequestMetricTests : TestBaseClass<RequestMetric>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestMetricTests"/> class.
        /// </summary>
        public RequestMetricTests()
        {
            TestObject = new RequestMetric();
            ObjectType = typeof(RequestMetric);
        }
    }
}