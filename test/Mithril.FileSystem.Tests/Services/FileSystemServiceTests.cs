using Mithril.FileSystem.Abstractions.Interfaces;
using Mithril.FileSystem.Services;
using Mithril.Tests.Helpers;

namespace Mithril.FileSystem.Tests.Services
{
    /// <summary>
    /// File system service tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;FileSystemService&gt;"/>
    public class FileSystemServiceTests : TestBaseClass<FileSystemService>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemServiceTests"/> class.
        /// </summary>
        public FileSystemServiceTests()
        {
            TestObject = new FileSystemService(null, Array.Empty<IPathConverter>());
            ObjectType = typeof(FileSystemService);
        }
    }
}