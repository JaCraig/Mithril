using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Mithril.API.Abstractions.Commands;
using Mithril.API.Abstractions.Commands.BaseClasses;
using Mithril.API.Abstractions.Commands.Interfaces;
using Mithril.API.Commands.Services;
using Mithril.Tests.Helpers;
using System.Security.Claims;

namespace Mithril.API.Commands.Tests.Services
{
    public class CommandServiceTests : TestBaseClass<CommandService>
    {
        public CommandServiceTests()
        {
            TestObject = new CommandService(new ICommandHandler[] { new TestCommandHandler(null, null) }, null, null, null, null);
            ObjectType = typeof(CommandService);
        }
    }

    internal class TestCommand : CommandBaseClass<TestCommand>
    {
        public TestCommand()
        { }
    }

    internal class TestCommandHandler : CommandHandlerBaseClass<TestCommand, TestCommandVM>
    {
        public TestCommandHandler(ILogger? logger, IFeatureManager? featureManager) : base(logger, featureManager)
        {
        }

        public override CommandCreationResult? Create(TestCommandVM? value, ClaimsPrincipal user)
        {
            return new CommandCreationResult(new TestCommand());
        }

        protected override IEvent[] HandleCommand(params TestCommand?[]? args)
        {
            return Array.Empty<IEvent>();
        }
    }

    internal class TestCommandVM
    {
    }
}