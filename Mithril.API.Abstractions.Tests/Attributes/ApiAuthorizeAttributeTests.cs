using Mithril.API.Abstractions.Attributes;
using Mithril.Tests.Helpers;

namespace Mithril.API.Abstractions.Tests.Attributes
{
    public class ApiAuthorizeAttributeTests : TestBaseClass<ApiAuthorizeAttribute>
    {
        public ApiAuthorizeAttributeTests()
        {
            TestObject = new ApiAuthorizeAttribute();
            ObjectType = typeof(ApiAuthorizeAttribute);
        }
    }
}