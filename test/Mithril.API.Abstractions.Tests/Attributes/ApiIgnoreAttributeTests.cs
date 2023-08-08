using Mithril.API.Abstractions.Attributes;
using Mithril.Tests.Helpers;

namespace Mithril.API.Abstractions.Tests.Attributes
{
    public class ApiIgnoreAttributeTests : TestBaseClass<ApiIgnoreAttribute>
    {
        public ApiIgnoreAttributeTests()
        {
            TestObject = new ApiIgnoreAttribute();
            ObjectType = typeof(ApiIgnoreAttribute);
        }
    }
}