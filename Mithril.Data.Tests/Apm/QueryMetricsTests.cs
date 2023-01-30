using Mithril.Data.Apm;
using Mithril.Tests.Helpers;

namespace Mithril.Data.Tests.Apm
{
    /// <summary>
    /// QueryMetrics tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;QueryMetrics&gt;"/>
    public class QueryMetricsTests : TestBaseClass<QueryMetrics>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryMetricsTests"/> class.
        /// </summary>
        public QueryMetricsTests()
        {
            TestObject = new QueryMetrics();
            ObjectType = typeof(QueryMetrics);
        }
    }
}