using Mithril.Tests.Helpers;

namespace Mithril.Apm.Abstractions.Tests
{
    /// <summary>
    /// Metrics entry tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;MetricsEntry&gt;"/>
    public class MetricsEntryTests : TestBaseClass<MetricsEntry>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MetricsEntryTests"/> class.
        /// </summary>
        public MetricsEntryTests()
        {
            TestObject = new MetricsEntry();
        }
    }
}