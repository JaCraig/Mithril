using Mithril.Communication.Admin.ViewModels;
using Mithril.Tests.Helpers;

namespace Mithril.Communication.Tests.Admin.ViewModels
{
    /// <summary>
    /// Message template VM tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;MessageTemplateVM&gt;" />
    public class MessageTemplateVMTests : TestBaseClass<MessageTemplateVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageTemplateVMTests"/> class.
        /// </summary>
        public MessageTemplateVMTests()
        {
            TestObject = new MessageTemplateVM();
            ObjectType = typeof(MessageTemplateVM);
        }
    }
}