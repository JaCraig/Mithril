using Mithril.Data.Models.General;
using Mithril.Tests.Helpers;

namespace Mithril.Data.Tests.Models.General
{
    /// <summary>
    /// Look up tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;LookUp&gt;" />
    public class LookUpTests : TestBaseClass<LookUp>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LookUpTests"/> class.
        /// </summary>
        public LookUpTests()
        {
            TestObject = new LookUp();
            ObjectType = typeof(LookUp);
        }
    }
}