using Mithril.Background.Default.Services;
using Mithril.Tests.Helpers;

namespace Mithril.Background.Default.Tests.Services
{
    /// <summary>
    /// BackgroundTaskService tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;BackgroundTaskService&gt;" />
    public class BackgroundTaskServiceTests : TestBaseClass<BackgroundTaskService>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BackgroundTaskServiceTests"/> class.
        /// </summary>
        public BackgroundTaskServiceTests()
        {
            TestObject = new BackgroundTaskService(null, null);
            ObjectType = typeof(BackgroundTaskService);
        }
    }
}