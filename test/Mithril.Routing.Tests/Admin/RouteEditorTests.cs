using Mithril.Routing.Admin;
using Mithril.Tests.Helpers;

namespace Mithril.Routing.Tests.Admin
{
    /// <summary>
    /// Route editor tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;RouteEditor&gt;" />
    public class RouteEditorTests : TestBaseClass<RouteEditor>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RouteEditorTests"/> class.
        /// </summary>
        public RouteEditorTests()
        {
            TestObject = new RouteEditor(null, null, null);
            ObjectType = typeof(RouteEditor);
        }
    }
}