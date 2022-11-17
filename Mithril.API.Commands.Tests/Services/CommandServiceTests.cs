using Mithril.API.Abstractions.Commands.Interfaces;
using Mithril.API.Commands.Services;
using Mithril.Tests.Helpers;

namespace Mithril.API.Commands.Tests.Services
{
    public class CommandServiceTests : TestBaseClass<CommandService>
    {
        public CommandServiceTests()
        {
            TestObject = new CommandService(new List<ICommandHandler>(), null, null);
            ObjectType = typeof(CommandService);
        }
    }
}