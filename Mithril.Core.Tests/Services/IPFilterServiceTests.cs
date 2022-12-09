using Mithril.Core.Services;
using Mithril.Tests.Helpers;

namespace Mithril.Core.Tests.Services
{
    public class IPFilterServiceTests : TestBaseClass<IPFilterService>
    {
        public IPFilterServiceTests()
        {
            TestObject = new IPFilterService(null, null);
            ObjectType = typeof(IPFilterService);
        }
    }
}