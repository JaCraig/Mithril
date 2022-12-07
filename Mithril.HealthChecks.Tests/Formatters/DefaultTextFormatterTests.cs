using Mithril.HealthChecks.Formatters;
using Mithril.Tests.Helpers;

namespace Mithril.HealthChecks.Tests.Formatters
{
    public class DefaultTextFormatterTests : TestBaseClass<DefaultTextFormatter>
    {
        public DefaultTextFormatterTests()
        {
            TestObject = new DefaultTextFormatter();
        }
    }
}