using Mithril.Apm.Default.Models;
using Mithril.Tests.Helpers;

namespace Mithril.Apm.Default.Tests.Models
{
    /// <summary>
    /// Request trace tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;RequestTrace&gt;"/>
    public class RequestTraceTests : TestBaseClass<RequestTrace>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestTraceTests"/> class.
        /// </summary>
        public RequestTraceTests()
        {
            TestObject = new RequestTrace();
            ObjectType = typeof(RequestTrace);
        }
    }
}