using Mithril.Apm.Default.Middleware;
using Mithril.Tests.Helpers;

namespace Mithril.Apm.Default.Tests.Middleware
{
    /// <summary>
    /// ApmMiddleware tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;ApmMiddleware&gt;"/>
    public class ApmMiddlewareTests : TestBaseClass<ApmMiddleware>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApmMiddlewareTests"/> class.
        /// </summary>
        public ApmMiddlewareTests()
        {
            TestObject = new ApmMiddleware(null);
            ObjectType = typeof(ApmMiddleware);
        }
    }
}