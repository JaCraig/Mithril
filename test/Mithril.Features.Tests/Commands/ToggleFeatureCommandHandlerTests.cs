using Mithril.Features.Commands;
using Mithril.Tests.Helpers;

namespace Mithril.Features.Tests.Commands
{
    public class ToggleFeatureCommandHandlerTests : TestBaseClass<ToggleFeatureCommandHandler>
    {
        public ToggleFeatureCommandHandlerTests()
        {
            TestObject = new ToggleFeatureCommandHandler(null, null, null);
            ObjectType = typeof(ToggleFeatureCommandHandler);
        }
    }
}