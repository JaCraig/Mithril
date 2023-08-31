using Mithril.Data.Models.General.Mappings;
using Mithril.Tests.Helpers;

namespace Mithril.Data.Tests.Models.General.Mappings
{
    /// <summary>
    /// Look up mapping tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;LookUpMapping&gt;" />
    public class LookUpMappingTests : TestBaseClass<LookUpMapping>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LookUpMappingTests"/> class.
        /// </summary>
        public LookUpMappingTests()
        {
            TestObject = new LookUpMapping();
        }
    }
}