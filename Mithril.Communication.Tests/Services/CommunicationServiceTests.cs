using Mithril.Communication.Abstractions.Interfaces;
using Mithril.Communication.Services;
using Mithril.Tests.Helpers;

namespace Mithril.Communication.Tests.Services
{
    /// <summary>
    /// Communication service tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;CommunicationService&gt;"/>
    public class CommunicationServiceTests : TestBaseClass<CommunicationService>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommunicationServiceTests"/> class.
        /// </summary>
        public CommunicationServiceTests()
        {
            TestObject = new CommunicationService(Array.Empty<IChannel>(), null);
        }
    }
}