using Mithril.Apm.Default.Queries;
using Mithril.Tests.Helpers;

namespace Mithril.Apm.Default.Tests.Queries
{
    /// <summary>
    /// RequestTraceQuery tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;RequestTraceQuery&gt;"/>
    public class RequestTraceQueryTests : TestBaseClass<RequestTraceQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestTraceQueryTests"/> class.
        /// </summary>
        public RequestTraceQueryTests()
        {
            TestObject = new RequestTraceQuery(null, null, null);
            ObjectType = typeof(RequestTraceQuery);
        }
    }
}