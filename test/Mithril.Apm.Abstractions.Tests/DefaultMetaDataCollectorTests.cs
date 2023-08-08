using Mithril.Tests.Helpers;

namespace Mithril.Apm.Abstractions.Tests
{
    /// <summary>
    /// Default meta data collector tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;DefaultMetaDataCollector&gt;"/>
    public class DefaultMetaDataCollectorTests : TestBaseClass<DefaultMetaDataCollector>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultMetaDataCollectorTests"/> class.
        /// </summary>
        public DefaultMetaDataCollectorTests()
        {
            TestObject = new DefaultMetaDataCollector();
            ObjectType = typeof(DefaultMetaDataCollector);
        }
    }
}