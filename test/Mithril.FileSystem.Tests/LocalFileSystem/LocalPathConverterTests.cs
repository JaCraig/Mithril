using Mithril.FileSystem.LocalFileSystem;
using Mithril.Tests.Helpers;

namespace Mithril.FileSystem.Tests.LocalFileSystem
{
    /// <summary>
    /// Local path converter tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;LocalPathConverter&gt;"/>
    public class LocalPathConverterTests : TestBaseClass<LocalPathConverter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalPathConverterTests"/> class.
        /// </summary>
        public LocalPathConverterTests()
        {
            TestObject = new LocalPathConverter(null);
            ObjectType = typeof(LocalPathConverter);
        }
    }
}