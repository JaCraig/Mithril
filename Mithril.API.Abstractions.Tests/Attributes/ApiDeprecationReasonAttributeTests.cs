using Mithril.API.Abstractions.Attributes;
using Mithril.Tests.Helpers;

namespace Mithril.API.Abstractions.Tests.Attributes
{
    public class ApiDeprecationReasonAttributeTests : TestBaseClass<ApiDepricationReasonAttribute>
    {
        public ApiDeprecationReasonAttributeTests()
        {
            TestObject = new ApiDepricationReasonAttribute();
            ObjectType = typeof(ApiDepricationReasonAttribute);
        }
    }
}