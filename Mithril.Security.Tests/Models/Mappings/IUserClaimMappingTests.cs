using Mithril.Security.Models.Mappings;
using Mithril.Tests.Helpers;

namespace Mithril.Security.Tests.Models.Mappings
{
    /// <summary>
    /// IUserClaimMapping tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;IUserClaimMapping&gt;"/>
    public class IUserClaimMappingTests : TestBaseClass<IUserClaimMapping>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IUserClaimMappingTests"/> class.
        /// </summary>
        public IUserClaimMappingTests()
        {
            TestObject = new IUserClaimMapping();
        }
    }
}