using Mithril.Communication.Email.HealthChecks;
using Mithril.Tests.Helpers;

namespace Mithril.Communication.Email.Tests.HealthChecks
{
    /// <summary>
    /// SMTP Health check tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;SMTPHealthCheck&gt;"/>
    public class SMTPHealthCheckTests : TestBaseClass<SMTPHealthCheck>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SMTPHealthCheckTests"/> class.
        /// </summary>
        public SMTPHealthCheckTests()
        {
            TestObject = new SMTPHealthCheck();
        }
    }
}