using Mithril.Admin.Commands.ViewModels;
using Mithril.Tests.Helpers;

namespace Mithril.Admin.Tests.Commands.ViewModels
{
    /// <summary>
    /// Save model command view model tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;SaveModelCommandVM&gt;" />
    public class SaveModelCommandVMTests : TestBaseClass<SaveModelCommandVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SaveModelCommandVMTests"/> class.
        /// </summary>
        public SaveModelCommandVMTests()
        {
            TestObject = new SaveModelCommandVM();
            ObjectType = typeof(SaveModelCommandVM);
        }
    }
}