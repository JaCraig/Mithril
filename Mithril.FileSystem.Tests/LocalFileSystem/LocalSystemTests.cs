using Mithril.FileSystem.LocalFileSystem;
using Mithril.Tests.Helpers;

namespace Mithril.FileSystem.Tests.LocalFileSystem
{
    /// <summary>
    /// Local system tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;LocalSystem&gt;"/>
    public class LocalSystemTests : TestBaseClass<LocalSystem>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalSystemTests"/> class.
        /// </summary>
        public LocalSystemTests()
        {
            TestObject = new LocalSystem(null);
            ObjectType = typeof(LocalSystem);
        }
    }
}