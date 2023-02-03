using Mithril.Caching.InMemory.Commands;
using Mithril.Tests.Helpers;

namespace Mithril.Caching.InMemory.Tests.Commands
{
    /// <summary>
    /// Clear cache command handler tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;ClearCacheCommandHandler&gt;"/>
    public class ClearCacheCommandHandlerTests : TestBaseClass<ClearCacheCommandHandler>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClearCacheCommandHandlerTests"/> class.
        /// </summary>
        public ClearCacheCommandHandlerTests()
        {
            TestObject = new ClearCacheCommandHandler(null, null);
            ObjectType = typeof(ClearCacheCommandHandler);
        }
    }
}