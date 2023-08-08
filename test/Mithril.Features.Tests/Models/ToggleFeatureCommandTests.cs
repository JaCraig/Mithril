using Mithril.Features.Models;
using Mithril.Tests.Helpers;

namespace Mithril.Features.Tests.Models
{
    public class ToggleFeatureCommandTests : TestBaseClass<ToggleFeatureCommand>
    {
        public ToggleFeatureCommandTests()
        {
            TestObject = new ToggleFeatureCommand();
            ObjectType = typeof(ToggleFeatureCommand);
        }
    }
}