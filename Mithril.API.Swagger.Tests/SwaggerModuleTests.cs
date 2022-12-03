using Mithril.Tests.Helpers;

namespace Mithril.API.Swagger.Tests
{
    public class SwaggerModuleTests : TestBaseClass<SwaggerModule>
    {
        public SwaggerModuleTests()
        {
            TestObject = new SwaggerModule();
            DiscoverInheritedMethods = true;
        }
    }
}