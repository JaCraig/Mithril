using Mithril.Tests.Helpers;

namespace Mithril.Apm.Abstractions.Tests
{
    /// <summary>
    /// Trace information tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;TraceInformation&gt;"/>
    public class TraceInformationTests : TestBaseClass<TraceInformation>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TraceInformationTests"/> class.
        /// </summary>
        public TraceInformationTests()
        {
            TestObject = new TraceInformation();
        }
    }
}