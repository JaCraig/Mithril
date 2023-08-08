using Mithril.Tests.Helpers;

namespace Mithril.Communication.Tests
{
    /// <summary>
    /// Communication module tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;CommunicationModule&gt;"/>
    public class CommunicationModuleTests : TestBaseClass<CommunicationModule>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommunicationModuleTests"/> class.
        /// </summary>
        public CommunicationModuleTests()
        {
            TestObject = new CommunicationModule();
        }
    }
}