using Mithril.Admin.Abstractions.Commands;
using Mithril.Tests.Helpers;

namespace Mithril.Admin.Abstractions.Tests.Commands
{
    /// <summary>
    /// Save model command tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;SaveModelCommand&gt;" />
    public class SaveModelCommandTests : TestBaseClass<SaveModelCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SaveModelCommandTests"/> class.
        /// </summary>
        public SaveModelCommandTests()
        {
            TestObject = new SaveModelCommand();
        }
    }
}