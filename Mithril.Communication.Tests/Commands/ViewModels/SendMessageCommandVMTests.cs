using Mithril.Communication.Commands.ViewModels;
using Mithril.Tests.Helpers;

namespace Mithril.Communication.Tests.Commands.ViewModels
{
    /// <summary>
    /// Send message command VM tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;SendMessageCommandVM&gt;"/>
    public class SendMessageCommandVMTests : TestBaseClass<SendMessageCommandVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SendMessageCommandVMTests"/> class.
        /// </summary>
        public SendMessageCommandVMTests()
        {
            TestObject = new SendMessageCommandVM();
        }
    }
}