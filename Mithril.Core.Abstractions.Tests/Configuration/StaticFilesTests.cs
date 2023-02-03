using Mithril.Core.Abstractions.Configuration;
using Mithril.Tests.Helpers;

namespace Mithril.Core.Abstractions.Tests.Configuration
{
    /// <summary>
    /// StaticFiles tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;StaticFiles&gt;"/>
    public class StaticFilesTests : TestBaseClass<StaticFiles>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StaticFilesTests"/> class.
        /// </summary>
        public StaticFilesTests()
        {
            TestObject = new StaticFiles();
        }
    }
}