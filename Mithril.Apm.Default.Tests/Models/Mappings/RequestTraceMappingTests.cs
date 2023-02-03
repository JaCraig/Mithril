using Mithril.Apm.Default.Models.Mappings;
using Mithril.Tests.Helpers;

namespace Mithril.Apm.Default.Tests.Models.Mappings
{
    /// <summary>
    /// RequestTraceMapping tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;RequestTraceMapping&gt;"/>
    public class RequestTraceMappingTests : TestBaseClass<RequestTraceMapping>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestTraceMappingTests"/> class.
        /// </summary>
        public RequestTraceMappingTests()
        {
            TestObject = new RequestTraceMapping();
        }
    }
}