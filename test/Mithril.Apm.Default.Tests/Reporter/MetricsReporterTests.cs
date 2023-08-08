using Mithril.Apm.Default.Reporter;
using Mithril.Tests.Helpers;

namespace Mithril.Apm.Default.Tests.Reporter
{
    public class MetricsReporterTests : TestBaseClass<MetricsReporter>
    {
        public MetricsReporterTests()
        {
            TestObject = new MetricsReporter(null);
            ObjectType = typeof(MetricsReporter);
        }
    }
}