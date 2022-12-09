using Mithril.Tests.Helpers;

namespace Mithril.Caching.InMemory.Tests
{
    public class InMemoryCachingModuleTests : TestBaseClass<InMemoryCachingModule>
    {
        public InMemoryCachingModuleTests()
        {
            TestObject = new InMemoryCachingModule();
        }
    }
}