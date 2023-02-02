using Mithril.Apm.Default.HostedServices;
using Mithril.Tests.Helpers;

namespace Mithril.Apm.Default.Tests.HostedServices
{
    /// <summary>
    /// Metrics reporter hosted service tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;MetricsReporterHostedService&gt;"/>
    public class MetricsReporterHostedServiceTests : TestBaseClass<MetricsReporterHostedService>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MetricsReporterHostedServiceTests"/> class.
        /// </summary>
        public MetricsReporterHostedServiceTests()
        {
            TestObject = new MetricsReporterHostedService(null, null, null, null);
            ObjectType = typeof(MetricsReporterHostedService);
        }
    }
}