using Mithril.Tests.Helpers;

namespace Mithril.Apm.Abstractions.Tests
{
    /// <summary>
    /// Default metrics collector tests
    /// </summary>
    /// <seealso cref="Mithril.Tests.Helpers.TestBaseClass&lt;Mithril.Apm.Abstractions.DefaultMetricsCollector&gt;"/>
    public class DefaultMetricsCollectorTests : TestBaseClass<DefaultMetricsCollector>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultMetricsCollectorTests"/> class.
        /// </summary>
        public DefaultMetricsCollectorTests()
        {
            TestObject = new DefaultMetricsCollector();
            ObjectType = typeof(DefaultMetricsCollector);
        }
    }
}