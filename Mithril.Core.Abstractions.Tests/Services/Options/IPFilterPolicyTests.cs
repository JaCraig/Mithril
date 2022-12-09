using Mithril.Core.Abstractions.Services.Options;
using Mithril.Tests.Helpers;

namespace Mithril.Core.Abstractions.Tests.Services.Options
{
    public class IPFilterPolicyTests : TestBaseClass<IPFilterPolicy>
    {
        public IPFilterPolicyTests()
        {
            TestObject = new IPFilterPolicy("Default");
            ObjectType = typeof(IPFilterPolicy);
        }
    }
}