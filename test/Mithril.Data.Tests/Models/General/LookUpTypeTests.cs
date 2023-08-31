using Mithril.Data.Models.General;
using Mithril.Tests.Helpers;

namespace Mithril.Data.Tests.Models.General
{
    /// <summary>
    /// Look up type tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;LookUpType&gt;" />
    public class LookUpTypeTests : TestBaseClass<LookUpType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LookUpTypeTests"/> class.
        /// </summary>
        public LookUpTypeTests()
        {
            TestObject = new LookUpType();
            ObjectType = typeof(LookUpType);
        }
    }
}