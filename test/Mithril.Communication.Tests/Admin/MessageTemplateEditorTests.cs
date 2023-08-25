using Mithril.Communication.Admin;
using Mithril.Tests.Helpers;

namespace Mithril.Communication.Tests.Admin
{
    /// <summary>
    /// Message template editor tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;MessageTemplateEditor&gt;" />
    public class MessageTemplateEditorTests : TestBaseClass<MessageTemplateEditor>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageTemplateEditorTests"/> class.
        /// </summary>
        public MessageTemplateEditorTests()
        {
            TestObject = new MessageTemplateEditor(null, null, null, null);
            ObjectType = typeof(MessageTemplateEditor);
        }
    }
}