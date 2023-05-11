using Mithril.Caching.InMemory.BackgroundTasks;
using Mithril.Tests.Helpers;

namespace Mithril.Caching.InMemory.Tests.BackgroundTasks
{
    public class MemoryCompactionServiceTests : TestBaseClass<MemoryCompactionScheduledTask>
    {
        public MemoryCompactionServiceTests()
        {
            TestObject = new MemoryCompactionScheduledTask(null);
            ObjectType = typeof(MemoryCompactionScheduledTask);
        }
    }
}