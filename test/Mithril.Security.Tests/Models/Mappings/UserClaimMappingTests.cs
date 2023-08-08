using Mithril.Security.Models.Mappings;
using Mithril.Tests.Helpers;

namespace Mithril.Security.Tests.Models.Mappings
{
    /// <summary>
    /// UserClaimMapping tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;UserClaimMapping&gt;"/>
    public class UserClaimMappingTests : TestBaseClass<UserClaimMapping>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserClaimMappingTests"/> class.
        /// </summary>
        public UserClaimMappingTests()
        {
            TestObject = new UserClaimMapping();
        }
    }
}