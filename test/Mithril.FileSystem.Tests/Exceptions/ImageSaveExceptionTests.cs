using Mithril.FileSystem.Exceptions;
using Mithril.Tests.Helpers;

namespace Mithril.FileSystem.Tests.Exceptions
{
    /// <summary>
    /// Image save exception tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;ImageSaveException&gt;" />
    public class ImageSaveExceptionTests : TestBaseClass<ImageSaveException>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageSaveExceptionTests"/> class.
        /// </summary>
        public ImageSaveExceptionTests()
        {
            TestObject = new ImageSaveException("Default");
        }
    }
}