using Mithril.HealthChecks.Abstractions.Interfaces;
using Mithril.HealthChecks.Services;
using Mithril.Tests.Helpers;

namespace Mithril.HealthChecks.Tests.Services
{
    public class ResponseFormatterServiceTests : TestBaseClass<ResponseFormatterService>
    {
        public ResponseFormatterServiceTests()
        {
            TestObject = new ResponseFormatterService(Array.Empty<IResponseFormatter>());
            ObjectType = typeof(ResponseFormatterService);
        }
    }
}