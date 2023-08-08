using Mithril.Admin.Controllers;
using Mithril.Tests.Helpers;

namespace Mithril.Admin.Tests.Controllers
{
    /// <summary>
    /// Admin controller tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;AdminController&gt;" />
    public class AdminControllerTests : TestBaseClass<AdminController>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdminControllerTests"/> class.
        /// </summary>
        public AdminControllerTests()
        {
            TestObject = new AdminController();
        }
    }
}