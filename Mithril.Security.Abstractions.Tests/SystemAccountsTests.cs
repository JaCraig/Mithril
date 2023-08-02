using Mithril.Tests.Helpers;

namespace Mithril.Security.Abstractions.Tests
{
    /// <summary>
    /// System accounts tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;SystemAccounts&gt;"/>
    public class SystemAccountsTests : TestBaseClass<SystemAccounts>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SystemAccountsTests"/> class.
        /// </summary>
        public SystemAccountsTests()
        {
            TestObject = new SystemAccounts();
        }
    }
}