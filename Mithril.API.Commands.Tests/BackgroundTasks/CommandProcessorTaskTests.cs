using Mithril.API.Commands.BackgroundTasks;
using Mithril.Tests.Helpers;

namespace Mithril.API.Commands.Tests.BackgroundTasks
{
    public class CommandProcessorTaskTests : TestBaseClass<CommandProcessorTask>
    {
        public CommandProcessorTaskTests()
        {
            TestObject = new CommandProcessorTask(null, null, null);
            ObjectType = typeof(CommandProcessorTask);
        }
    }
}