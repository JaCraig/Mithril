using Mithril.Admin.Queries.ViewModels;
using Mithril.Tests.Helpers;

namespace Mithril.Admin.Tests.Queries.ViewModels
{
    /// <summary>
    /// Editor view model tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;EditorVM&gt;" />
    public class EditorVMTests : TestBaseClass<EditorVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditorVMTests"/> class.
        /// </summary>
        public EditorVMTests()
        {
            TestObject = new EditorVM(null);
            ObjectType = typeof(EditorVM);
        }
    }
}