using Mithril.API.Abstractions.Commands.Interfaces;
using Mithril.API.Commands.Services;
using Mithril.Tests.Helpers;

namespace Mithril.API.Commands.Tests.Services
{
    public class EventServiceTests : TestBaseClass<EventService>
    {
        public EventServiceTests()
        {
            TestObject = new EventService(new List<IEventHandler>(), null, null);
            ObjectType = typeof(EventService);
        }
    }
}