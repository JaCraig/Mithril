using Mithril.Routing.Transformers;
using Mithril.Tests.Helpers;

namespace Mithril.Routing.Tests.Transformers
{
    /// <summary>
    /// RouteTransformer tests
    /// </summary>
    /// <seealso cref="Mithril.Tests.Helpers.TestBaseClass&lt;Mithril.Routing.Transformers.RouteTransformer&gt;"/>
    public class RouteTransformerTests : TestBaseClass<RouteTransformer>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RouteTransformerTests"/> class.
        /// </summary>
        public RouteTransformerTests()
        {
            TestObject = new RouteTransformer(null);
            ObjectType = typeof(RouteTransformer);
        }
    }
}