using Mithril.Tests.Helpers;

namespace Mithril.FileSystem.Tests
{
    /// <summary>
    /// File system module tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;FileSystemModule&gt;"/>
    public class FileSystemModuleTests : TestBaseClass<FileSystemModule>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemModuleTests"/> class.
        /// </summary>
        public FileSystemModuleTests()
        {
            TestObject = new FileSystemModule();
        }
    }
}