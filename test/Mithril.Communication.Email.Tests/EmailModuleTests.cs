using Mithril.Tests.Helpers;

namespace Mithril.Communication.Email.Tests
{
    /// <summary>
    /// Email module tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;EmailModule&gt;"/>
    public class EmailModuleTests : TestBaseClass<EmailModule>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailModuleTests"/> class.
        /// </summary>
        public EmailModuleTests()
        {
            TestObject = new EmailModule();
        }
    }
}