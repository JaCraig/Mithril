using Mithril.Tests.Helpers;

namespace Mithril.Mvc.Tests
{
    /// <summary>
    /// MVC Module tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;MvcModule&gt;"/>
    public class MvcModuleTests : TestBaseClass<MvcModule>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MvcModuleTests"/> class.
        /// </summary>
        public MvcModuleTests()
        {
            TestObject = new MvcModule();
        }
    }
}