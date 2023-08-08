using Mithril.Tests.Helpers;

namespace Mithril.Core.Tests
{
    /// <summary>
    /// Application tests
    /// </summary>
    /// <seealso cref="Mithril.Tests.Helpers.TestBaseClass&lt;Mithril.Core.Application&gt;"/>
    public class ApplicationTests : TestBaseClass<Application>
    {
        public ApplicationTests()
        {
            TestObject = new Application(null, null);
        }
    }
}