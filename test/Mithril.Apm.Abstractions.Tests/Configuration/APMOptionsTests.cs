using Mithril.Apm.Abstractions.Configuration;
using Mithril.Tests.Helpers;

namespace Mithril.Apm.Abstractions.Tests.Configuration
{
    /// <summary>
    /// APM Options tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;APMOptions&gt;"/>
    public class APMOptionsTests : TestBaseClass<APMOptions>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="APMOptionsTests"/> class.
        /// </summary>
        public APMOptionsTests()
        {
            TestObject = new APMOptions();
        }
    }
}