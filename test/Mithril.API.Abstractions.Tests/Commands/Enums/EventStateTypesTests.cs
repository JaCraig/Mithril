using Mithril.API.Abstractions.Commands.Enums;
using Mithril.Tests.Helpers;

namespace Mithril.API.Abstractions.Tests.Commands.Enums
{
    /// <summary>
    /// EventStateTypes tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;EventStateTypes&gt;"/>
    public class EventStateTypesTests : TestBaseClass<EventStateTypes>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventStateTypesTests"/> class.
        /// </summary>
        public EventStateTypesTests()
        {
            TestObject = new EventStateTypes();
            ObjectType = typeof(EventStateTypes);
        }
    }
}