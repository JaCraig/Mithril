using Mithril.Caching.InMemory.Commands.ViewModels;
using Mithril.Tests.Helpers;

namespace Mithril.Caching.InMemory.Tests.Commands.ViewModels
{
    /// <summary>
    /// ClearCacheCommandVM tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;ClearCacheCommandVM&gt;"/>
    public class ClearCacheCommandVMTests : TestBaseClass<ClearCacheCommandVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClearCacheCommandVMTests"/> class.
        /// </summary>
        public ClearCacheCommandVMTests()
        {
            TestObject = new ClearCacheCommandVM();
        }
    }
}