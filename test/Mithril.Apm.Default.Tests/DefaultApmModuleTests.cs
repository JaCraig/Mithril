using Mithril.Tests.Helpers;

namespace Mithril.Apm.Default.Tests
{
    /// <summary>
    /// Default APM module tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;DefaultApmModule&gt;"/>
    public class DefaultApmModuleTests : TestBaseClass<DefaultApmModule>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultApmModuleTests"/> class.
        /// </summary>
        public DefaultApmModuleTests()
        {
            TestObject = new DefaultApmModule();
            ObjectType = typeof(DefaultApmModule);
        }
    }
}