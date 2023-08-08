using Mithril.Background.Default.HostedServices;
using Mithril.Tests.Helpers;

namespace Mithril.Background.Default.Tests.HostedServices
{
    /// <summary>
    /// BackgroundTasksHostedService tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;BackgroundTasksHostedService&gt;" />
    public class BackgroundTasksHostedServiceTests : TestBaseClass<BackgroundTasksHostedService>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BackgroundTasksHostedServiceTests"/> class.
        /// </summary>
        public BackgroundTasksHostedServiceTests()
        {
            TestObject = new BackgroundTasksHostedService(null, null);
            ObjectType = typeof(BackgroundTasksHostedService);
        }
    }
}