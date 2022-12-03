using Mithril.API.Abstractions.Attributes;
using Mithril.Tests.Helpers;

namespace Mithril.API.Abstractions.Tests.Attributes
{
    public class ApiAllowAnonymousAttributeTests : TestBaseClass<ApiAllowAnonymousAttribute>
    {
        public ApiAllowAnonymousAttributeTests()
        {
            TestObject = new ApiAllowAnonymousAttribute();
            ObjectType = typeof(ApiAllowAnonymousAttribute);
        }
    }
}