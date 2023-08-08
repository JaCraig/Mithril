using Mithril.Tests.Helpers;

namespace Mithril.Security.Windows.Tests
{
    public class WindowsAuthenticationModuleTests : TestBaseClass<WindowsAuthenticationModule>
    {
        public WindowsAuthenticationModuleTests()
        {
            TestObject = new WindowsAuthenticationModule();
            ObjectType = typeof(WindowsAuthenticationModule);
            DiscoverInheritedMethods = true;
        }
    }
}