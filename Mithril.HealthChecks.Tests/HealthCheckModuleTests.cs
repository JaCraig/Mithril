using Mithril.Tests.Helpers;

namespace Mithril.HealthChecks.Tests
{
    public class HealthCheckModuleTests : TestBaseClass<HealthCheckModule>
    {
        public HealthCheckModuleTests()
        {
            TestObject = new HealthCheckModule();
        }
    }
}