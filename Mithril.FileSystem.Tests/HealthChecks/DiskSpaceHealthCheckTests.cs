using Mithril.FileSystem.HealthChecks;
using Mithril.Tests.Helpers;

namespace Mithril.FileSystem.Tests.HealthChecks
{
    /// <summary>
    /// Disk space health check tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;DiskSpaceHealthCheck&gt;"/>
    public class DiskSpaceHealthCheckTests : TestBaseClass<DiskSpaceHealthCheck>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DiskSpaceHealthCheckTests"/> class.
        /// </summary>
        public DiskSpaceHealthCheckTests()
        {
            TestObject = new DiskSpaceHealthCheck();
        }
    }
}