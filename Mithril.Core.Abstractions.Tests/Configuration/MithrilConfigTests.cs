using Mithril.Core.Abstractions.Configuration;
using Mithril.Tests.Helpers;

namespace Mithril.Core.Abstractions.Tests.Configuration
{
    /// <summary>
    /// MithrilConfig tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;MithrilConfig&gt;"/>
    public class MithrilConfigTests : TestBaseClass<MithrilConfig>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MithrilConfigTests"/> class.
        /// </summary>
        public MithrilConfigTests()
        {
            TestObject = new MithrilConfig();
        }
    }
}