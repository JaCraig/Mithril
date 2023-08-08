using Mithril.Core.Abstractions.Configuration;
using Mithril.Tests.Helpers;

namespace Mithril.Core.Abstractions.Tests.Configuration
{
    /// <summary>
    /// Security tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;Security&gt;"/>
    public class SecurityTests : TestBaseClass<Security>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityTests"/> class.
        /// </summary>
        public SecurityTests()
        {
            TestObject = new Security();
        }
    }
}