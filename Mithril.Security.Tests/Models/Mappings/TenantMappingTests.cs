using Mithril.Security.Models.Mappings;
using Mithril.Tests.Helpers;

namespace Mithril.Security.Tests.Models.Mappings
{
    /// <summary>
    /// TenantMapping tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;TenantMapping&gt;"/>
    public class TenantMappingTests : TestBaseClass<TenantMapping>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TenantMappingTests"/> class.
        /// </summary>
        public TenantMappingTests()
        {
            TestObject = new TenantMapping();
        }
    }
}