using Mithril.Apm.Default.BackgroundTasks;
using Mithril.Tests.Helpers;

namespace Mithril.Apm.Default.Tests.BackgroundTasks
{
    /// <summary>
    /// Metrics reporter hosted service tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;MetricsReporterHostedService&gt;"/>
    public class MetricsReporterHostedServiceTests : TestBaseClass<MetricsReporterBackgroundTask>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MetricsReporterHostedServiceTests"/> class.
        /// </summary>
        public MetricsReporterHostedServiceTests()
        {
            TestObject = new MetricsReporterBackgroundTask(null, null, null, null);
            ObjectType = typeof(MetricsReporterBackgroundTask);
        }
    }
}