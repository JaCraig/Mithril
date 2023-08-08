using Mithril.Security.Models.Mappings;
using Mithril.Tests.Helpers;

namespace Mithril.Security.Tests.Models.Mappings
{
    /// <summary>
    /// UserMapping tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;UserMapping&gt;"/>
    public class UserMappingTests : TestBaseClass<UserMapping>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserMappingTests"/> class.
        /// </summary>
        public UserMappingTests()
        {
            TestObject = new UserMapping();
        }
    }
}