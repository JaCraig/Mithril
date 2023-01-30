using Mithril.Apm.Abstractions.Interfaces;
using Mithril.Apm.Default.Services;
using Mithril.Tests.Helpers;

namespace Mithril.Apm.Default.Tests.Services
{
    public class MetricsCollectorServiceTests : TestBaseClass<MetricsCollectorService>
    {
        public MetricsCollectorServiceTests()
        {
            TestObject = new MetricsCollectorService(Array.Empty<IMetricsCollector>(), Array.Empty<IMetricsReporter>(), Array.Empty<IMetaDataCollector>(), Array.Empty<IEventListener>(), null, null);
            ObjectType = typeof(MetricsCollectorService);
        }
    }
}