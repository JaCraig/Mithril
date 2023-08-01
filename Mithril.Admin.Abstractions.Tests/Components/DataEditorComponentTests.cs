using Mithril.Admin.Abstractions.Components;
using Mithril.Tests.Helpers;

namespace Mithril.Admin.Abstractions.Tests.Components
{
    /// <summary>
    /// DataEditor component tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;DataEditorComponent&lt;TestEditor&gt;&gt;" />
    public class DataEditorComponentTests : TestBaseClass<DataEditorComponent<TestEditor>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataEditorComponentTests"/> class.
        /// </summary>
        public DataEditorComponentTests()
        {
            TestObject = new DataEditorComponent<TestEditor>();
        }
    }

    /// <summary>
    /// Test editor
    /// </summary>
    public class TestEditor
    { }
}