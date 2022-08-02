using Mithril.Core.Modules;
using Mithril.Tests.Helpers;

namespace Mithril.Core.Tests.Modules
{
    public class MithrilModuleTests : TestBaseClass<MithrilModule>
    {
        public MithrilModuleTests()
        {
            TestObject = new MithrilModule();
            ExceptionsToIgnore = new Type[] { typeof(InvalidOperationException) };
        }
    }
}