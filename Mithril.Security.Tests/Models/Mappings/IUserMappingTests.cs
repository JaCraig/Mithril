using Mithril.Security.Models.Mappings;
using Mithril.Tests.Helpers;

namespace Mithril.Security.Tests.Models.Mappings
{
    /// <summary>
    /// IUserMapping tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;IUserMapping&gt;"/>
    public class IUserMappingTests : TestBaseClass<IUserMapping>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IUserMappingTests"/> class.
        /// </summary>
        public IUserMappingTests()
        {
            TestObject = new IUserMapping();
        }
    }
}