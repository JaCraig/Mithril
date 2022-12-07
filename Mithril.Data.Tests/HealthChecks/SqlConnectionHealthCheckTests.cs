using Mithril.Data.HealthCheck;
using Mithril.Tests.Helpers;

namespace Mithril.Data.Tests.HealthChecks
{
    public class SqlConnectionHealthCheckTests : TestBaseClass<SqlConnectionHealthCheck>
    {
        public SqlConnectionHealthCheckTests()
        {
            TestObject = new SqlConnectionHealthCheck(null);
            ObjectType = typeof(SqlConnectionHealthCheck);
        }
    }
}