using Mithril.Features.Services;
using Mithril.Tests.Helpers;

namespace Mithril.Features.Tests.Services
{
    public class DatabaseSessionManagerTests : TestBaseClass<DatabaseSessionManager>
    {
        public DatabaseSessionManagerTests()
        {
            TestObject = new DatabaseSessionManager(null);
            ObjectType = typeof(DatabaseSessionManager);
        }
    }
}