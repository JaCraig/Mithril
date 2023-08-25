using Mithril.FileSystem.ViewModels;
using Mithril.Tests.Helpers;

namespace Mithril.FileSystem.Tests.ViewModels
{
    /// <summary>
    /// File browser directory VM tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;FileBrowserDirectoryVM&gt;" />
    public class FileBrowserDirectoryVMTests : TestBaseClass<FileBrowserDirectoryVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileBrowserDirectoryVMTests"/> class.
        /// </summary>
        public FileBrowserDirectoryVMTests()
        {
            TestObject = new FileBrowserDirectoryVM(null, null, null, null);
            ObjectType = typeof(FileBrowserDirectoryVM);
        }
    }
}