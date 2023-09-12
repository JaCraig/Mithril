using Mithril.Data.Admin;
using Mithril.Tests.Helpers;

namespace Mithril.Data.Tests.Admin
{
    /// <summary>
    /// LookUpTypeEditor Tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;LookUpTypeEditor&gt;" />
    public class LookUpTypeEditorTests : TestBaseClass<LookUpTypeEditor>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LookUpTypeEditorTests"/> class.
        /// </summary>
        public LookUpTypeEditorTests()
        {
            TestObject = new LookUpTypeEditor(null, null, null);
            ObjectType = typeof(LookUpTypeEditor);
        }
    }
}