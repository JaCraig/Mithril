using Mithril.HealthChecks.HealthChecks;
using Mithril.Tests.Helpers;

namespace Mithril.HealthChecks.Tests.HealthChecks
{
    public class SystemStatusHealthCheckTests : TestBaseClass<SystemStatusHealthCheck>
    {
        public SystemStatusHealthCheckTests()
        {
            TestObject = new SystemStatusHealthCheck(null);
            ObjectType = typeof(SystemStatusHealthCheck);
        }
    }
}