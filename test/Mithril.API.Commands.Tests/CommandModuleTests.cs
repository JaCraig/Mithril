using Mithril.Tests.Helpers;

namespace Mithril.API.Commands.Tests
{
    public class CommandModuleTests : TestBaseClass<CommandModule>
    {
        public CommandModuleTests()
        {
            TestObject = new CommandModule();
            DiscoverInheritedMethods = true;
        }
    }
}