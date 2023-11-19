using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Mithril.API.Abstractions.Commands;
using Mithril.API.Abstractions.Commands.BaseClasses;
using Mithril.API.Abstractions.Commands.Enums;
using Mithril.API.Abstractions.Commands.Interfaces;
using Mithril.API.Commands.Services;
using Mithril.Tests.Helpers;
using System.Dynamic;

namespace Mithril.API.Commands.Tests.Services
{
    public class EventServiceTests : TestBaseClass<EventService>
    {
        public EventServiceTests()
        {
            TestObject = new EventService(new IEventHandler[] { new TestEventHandler(null, null) }, null, null, null, null);
            ObjectType = typeof(EventService);
        }
    }

    internal class TestEvent : EventBaseClass<TestEvent>
    {
        public override ExpandoObject GetData() => new();

        public override string GetSchema() => "";
    }

    internal class TestEventHandler(ILogger<TestEventHandler>? logger, IFeatureManager? featureManager) : EventHandlerBaseClass<TestEventHandler, TestEvent>(logger, featureManager)
    {
        protected override EventResult Handle(TestEvent arg) => new(arg, EventStateTypes.Completed, this);
    }
}