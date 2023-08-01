using Mithril.Admin.Commands;
using Mithril.Tests.Helpers;

namespace Mithril.Admin.Tests.Commands
{
    /// <summary>
    /// Save model command handler tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;SaveModelCommandHandler&gt;" />
    public class SaveModelCommandHandlerTests : TestBaseClass<SaveModelCommandHandler>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SaveModelCommandHandlerTests"/> class.
        /// </summary>
        public SaveModelCommandHandlerTests()
        {
            TestObject = new SaveModelCommandHandler(null, null, null);
            ObjectType = typeof(SaveModelCommandHandler);
        }
    }
}