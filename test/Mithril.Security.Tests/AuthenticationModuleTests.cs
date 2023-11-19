using Mithril.Tests.Helpers;

namespace Mithril.Security.Tests
{
    public class AuthenticationModuleTests : TestBaseClass<AuthenticationModule>
    {
        public AuthenticationModuleTests()
        {
            TestObject = new AuthenticationModule();
            ExceptionsToIgnore = [typeof(AggregateException), typeof(InvalidOperationException)];
            DiscoverInheritedMethods = true;
        }
    }
}