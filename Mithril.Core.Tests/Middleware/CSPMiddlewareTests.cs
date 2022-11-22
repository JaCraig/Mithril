using Mithril.Core.Middleware;
using Mithril.Tests.Helpers;

namespace Mithril.Core.Tests.Middleware
{
    /// <summary>
    /// CSP Middleware tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;CSPMiddleware&gt;"/>
    public class CSPMiddlewareTests : TestBaseClass<CSPMiddleware>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSPMiddlewareTests"/> class.
        /// </summary>
        public CSPMiddlewareTests()
        {
            TestObject = new CSPMiddleware(null, null);
            ObjectType = typeof(CSPMiddleware);
        }
    }
}