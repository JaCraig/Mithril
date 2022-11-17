using Mithril.API.Commands.BackgroundTasks;
using Mithril.Tests.Helpers;

namespace Mithril.API.Commands.Tests.BackgroundTasks
{
    public class EventProcessorTaskTests : TestBaseClass<EventProcessorTask>
    {
        public EventProcessorTaskTests()
        {
            TestObject = new EventProcessorTask(null, null, null);
            ObjectType = typeof(EventProcessorTask);
        }
    }
}