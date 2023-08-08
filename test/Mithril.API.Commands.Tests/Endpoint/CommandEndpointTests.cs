using Mithril.API.Commands.Endpoint;
using Mithril.Tests.Helpers;

namespace Mithril.API.Commands.Tests.Endpoint
{
    public class CommandEndpointTests : TestBaseClass
    {
        protected override Type? ObjectType { get; set; } = typeof(CommandEndpoint);
    }
}