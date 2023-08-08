using Mithril.Admin.Abstractions.DataEditor.Attributes;
using Mithril.Tests.Helpers;

namespace Mithril.Admin.Abstractions.Tests.DataEditor.Attributes
{
    /// <summary>
    /// Upload attribute tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;UploadAttribute&gt;" />
    public class UploadAttributeTests : TestBaseClass<UploadAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UploadAttributeTests"/> class.
        /// </summary>
        public UploadAttributeTests()
        {
            TestObject = new UploadAttribute("test");
            ObjectType = typeof(UploadAttribute);
        }
    }
}