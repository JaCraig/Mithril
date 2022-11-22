using Mithril.Core.Middleware;
using Mithril.Tests.Helpers;

namespace Mithril.Core.Tests.Middleware
{
    /// <summary>
    /// XFrame options middleware tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;XFrameOptionsMiddleware&gt;"/>
    public class XFrameOptionsMiddlewareTests : TestBaseClass<XFrameOptionsMiddleware>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XFrameOptionsMiddlewareTests"/> class.
        /// </summary>
        public XFrameOptionsMiddlewareTests()
        {
            TestObject = new XFrameOptionsMiddleware(null, null);
            ObjectType = typeof(XFrameOptionsMiddleware);
        }
    }
}