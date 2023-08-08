using Mithril.Core.Abstractions.Configuration;
using Mithril.Tests.Helpers;

namespace Mithril.Core.Abstractions.Tests.Configuration
{
    /// <summary>
    /// Compression tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;Compression&gt;"/>
    public class CompressionTests : TestBaseClass<Compression>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompressionTests"/> class.
        /// </summary>
        public CompressionTests()
        {
            TestObject = new Compression();
        }
    }
}