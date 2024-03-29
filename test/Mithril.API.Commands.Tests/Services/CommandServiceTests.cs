﻿using Microsoft.Extensions.Logging;
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

    internal class TestCommandHandler(ILogger? logger, IFeatureManager? featureManager) : CommandHandlerBaseClass<TestCommand, TestCommandVM>(logger, featureManager)
    {
        public override ValueTask<CommandCreationResult?> CreateAsync(TestCommandVM? value, ClaimsPrincipal user) => ValueTask.FromResult<CommandCreationResult?>(new CommandCreationResult(new TestCommand()));

        protected override Task<IEvent[]> HandleCommandAsync(TestCommand?[]? args) => Task.FromResult(Array.Empty<IEvent>());
    }

    internal class TestCommandVM
    {
    }
}