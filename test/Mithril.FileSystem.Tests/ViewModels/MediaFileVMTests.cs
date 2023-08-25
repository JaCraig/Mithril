using Mithril.FileSystem.ViewModels;
using Mithril.Tests.Helpers;

namespace Mithril.FileSystem.Tests.ViewModels
{
    /// <summary>
    /// Media file VM tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;MediaFileVM&gt;" />
    public class MediaFileVMTests : TestBaseClass<MediaFileVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaFileVMTests"/> class.
        /// </summary>
        public MediaFileVMTests()
        {
            TestObject = new MediaFileVM();
            ObjectType = typeof(MediaFileVM);
        }
    }
}