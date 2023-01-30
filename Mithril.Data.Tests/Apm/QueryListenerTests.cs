using Mithril.Data.Apm;
using Mithril.Tests.Helpers;

namespace Mithril.Data.Tests.Apm
{
    /// <summary>
    /// QueryListener tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;QueryListener&gt;"/>
    public class QueryListenerTests : TestBaseClass<QueryListener>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryListenerTests"/> class.
        /// </summary>
        public QueryListenerTests()
        {
            TestObject = new QueryListener(null, null);
            ObjectType = typeof(QueryListener);
        }
    }
}