using Mithril.Data.Abstractions.Databases;
using Mithril.Tests.Helpers;

namespace Mithril.Data.Abstractions.Tests.Databases
{
    /// <summary>
    /// DefaultDatabase tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;DefaultDatabase&gt;"/>
    public class DefaultDatabaseTests : TestBaseClass<DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultDatabaseTests"/> class.
        /// </summary>
        public DefaultDatabaseTests()
        {
            TestObject = new DefaultDatabase();
        }
    }
}