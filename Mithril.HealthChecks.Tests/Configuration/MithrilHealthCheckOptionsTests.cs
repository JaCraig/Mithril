using Mithril.HealthChecks.Abstractions.Configuration;
using Mithril.Tests.Helpers;

namespace Mithril.HealthChecks.Tests.Configuration
{
    /// <summary>
    /// MithrilHealthCheckOptions tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;MithrilHealthCheckOptions&gt;"/>
    public class MithrilHealthCheckOptionsTests : TestBaseClass<MithrilHealthCheckOptions>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MithrilHealthCheckOptionsTests"/> class.
        /// </summary>
        public MithrilHealthCheckOptionsTests()
        {
            TestObject = new MithrilHealthCheckOptions();
        }
    }
}