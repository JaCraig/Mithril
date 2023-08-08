using Mithril.API.Commands.Utils;
using Mithril.Tests.Helpers;

namespace Mithril.API.Commands.Tests.Utils
{
    /// <summary>
    /// Command endpoint builder tests
    /// </summary>
    /// <seealso cref="TestBaseClass"/>
    public class CommandEndpointBuilderTests : TestBaseClass
    {
        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        protected override Type? ObjectType { get; set; } = typeof(CommandEndpointBuilder);
    }
}