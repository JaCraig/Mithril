using Mithril.Tests.Helpers;
using Mithril.Themes.LocationExpanders;

namespace Mithril.Themes.Tests.LocationExpanders
{
    /// <summary>
    /// ModuleViewLocationExpanderProvider tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;ModuleViewLocationExpanderProvider&gt;" />
    public class ModuleViewLocationExpanderProviderTests : TestBaseClass<ModuleViewLocationExpanderProvider>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleViewLocationExpanderProviderTests"/> class.
        /// </summary>
        public ModuleViewLocationExpanderProviderTests()
        {
            TestObject = new ModuleViewLocationExpanderProvider(null);
            ObjectType = typeof(ModuleViewLocationExpanderProvider);
        }
    }
}