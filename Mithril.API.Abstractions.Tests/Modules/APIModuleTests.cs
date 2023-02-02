using Mithril.API.Abstractions.Modules;
using Mithril.Tests.Helpers;

namespace Mithril.API.Abstractions.Tests.Modules
{
    /// <summary>
    /// APIModule tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;APIModule&gt;"/>
    public class APIModuleTests : TestBaseClass<APIModule>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="APIModuleTests"/> class.
        /// </summary>
        public APIModuleTests()
        {
            TestObject = new APIModule();
        }
    }
}