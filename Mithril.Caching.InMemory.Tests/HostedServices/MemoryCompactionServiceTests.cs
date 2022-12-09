using Mithril.Caching.InMemory.HostedServices;
using Mithril.Tests.Helpers;

namespace Mithril.Caching.InMemory.Tests.HostedServices
{
    public class MemoryCompactionServiceTests : TestBaseClass<MemoryCompactionService>
    {
        public MemoryCompactionServiceTests()
        {
            TestObject = new MemoryCompactionService(null, null);
            ObjectType = typeof(MemoryCompactionService);
        }
    }
}