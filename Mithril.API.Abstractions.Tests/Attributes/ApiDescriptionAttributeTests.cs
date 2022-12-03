using Mithril.API.Abstractions.Attributes;
using Mithril.Tests.Helpers;

namespace Mithril.API.Abstractions.Tests.Attributes
{
    public class ApiDescriptionAttributeTests : TestBaseClass<ApiDescriptionAttribute>
    {
        public ApiDescriptionAttributeTests()
        {
            TestObject = new ApiDescriptionAttribute();
            ObjectType = typeof(ApiDescriptionAttribute);
        }
    }
}