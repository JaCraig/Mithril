using Mithril.FileSystem.Controllers;
using Mithril.Tests.Helpers;

namespace Mithril.FileSystem.Tests.Controllers
{
    /// <summary>
    /// File browser controller tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;FileBrowserController&gt;" />
    public class FileBrowserControllerTests : TestBaseClass<FileBrowserController>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileBrowserControllerTests"/> class.
        /// </summary>
        public FileBrowserControllerTests()
        {
            TestObject = new FileBrowserController(null);
            ObjectType = typeof(FileBrowserController);
        }
    }
}