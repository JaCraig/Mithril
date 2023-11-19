using Mithril.API.Abstractions.Commands;
using Mithril.API.Abstractions.Commands.Enums;
using Mithril.API.Abstractions.Commands.Interfaces;
using Mithril.Tests.Helpers;
using NSubstitute;
using Xunit;

namespace Mithril.API.Abstractions.Tests.Commands
{
    /// <summary>
    /// Event result tests
    /// </summary>
    /// <seealso cref="TestBaseClass"/>
    public class EventResultTests : TestBaseClass
    {
        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        protected override Type? ObjectType { get; set; } = typeof(EventResult);

        /// <summary>
        /// Constructor test.
        /// </summary>
        [Fact]
        public void ConstructorTest()
        {
            var TestObject = new EventResult(null, EventStateTypes.Completed, Substitute.For<IEventHandler>(), null);
            Assert.NotNull(TestObject);
        }
    }
}