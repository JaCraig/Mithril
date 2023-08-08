using Mithril.Admin.Abstractions.Components;
using Mithril.Tests.Helpers;

namespace Mithril.Admin.Abstractions.Tests.Components
{
    /// <summary>
    /// Settings editor component tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;SettingsEditorComponent&lt;TestEditor&gt;&gt;" />
    public class SettingsEditorComponentTests : TestBaseClass<SettingsEditorComponent<TestEditor>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsEditorComponentTests"/> class.
        /// </summary>
        public SettingsEditorComponentTests()
        {
            TestObject = new SettingsEditorComponent<TestEditor>();
        }
    }
}