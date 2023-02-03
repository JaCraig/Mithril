using Mithril.Security.Models.Mappings;
using Mithril.Tests.Helpers;

namespace Mithril.Security.Tests.Models.Mappings
{
    /// <summary>
    /// IPermissionMapping tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;IPermissionMapping&gt;"/>
    public class IPermissionMappingTests : TestBaseClass<IPermissionMapping>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IPermissionMappingTests"/> class.
        /// </summary>
        public IPermissionMappingTests()
        {
            TestObject = new IPermissionMapping();
        }
    }
}