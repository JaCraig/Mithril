using Mithril.HealthChecks.Formatters;
using Mithril.Tests.Helpers;

namespace Mithril.HealthChecks.Tests.Formatters
{
    public class DefaultJsonFormatterTests : TestBaseClass<DefaultJsonFormatter>
    {
        public DefaultJsonFormatterTests()
        {
            TestObject = new DefaultJsonFormatter();
        }
    }
}