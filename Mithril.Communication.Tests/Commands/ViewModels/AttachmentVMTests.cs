using Mithril.Communication.Commands.ViewModels;
using Mithril.Tests.Helpers;

namespace Mithril.Communication.Tests.Commands.ViewModels
{
    /// <summary>
    /// Attachment VM Tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;AttachmentVM&gt;"/>
    public class AttachmentVMTests : TestBaseClass<AttachmentVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttachmentVMTests"/> class.
        /// </summary>
        public AttachmentVMTests()
        {
            TestObject = new AttachmentVM();
        }
    }
}