using Mithril.Core.Middleware;
using Mithril.Tests.Helpers;

namespace Mithril.Core.Tests.Middleware
{
    /// <summary>
    /// IP Filter middleware tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;IPFilterMiddleware&gt;"/>
    public class IPFilterMiddlewareTests : TestBaseClass<IPFilterMiddleware>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IPFilterMiddlewareTests"/> class.
        /// </summary>
        public IPFilterMiddlewareTests()
        {
            TestObject = new IPFilterMiddleware(null, null, null);
            ObjectType = typeof(IPFilterMiddleware);
        }
    }
}