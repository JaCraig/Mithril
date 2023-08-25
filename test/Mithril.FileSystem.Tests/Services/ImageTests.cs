using Mithril.FileSystem.Services;
using Mithril.Tests.Helpers;

namespace Mithril.FileSystem.Tests.Services
{
    /// <summary>
    /// Image tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;Image&gt;" />
    public class ImageTests : TestBaseClass<Image>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageTests"/> class.
        /// </summary>
        public ImageTests()
        {
            TestObject = new Image(null);
            ObjectType = typeof(Image);
        }
    }
}