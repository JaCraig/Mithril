using Mithril.Features.Admin;
using Mithril.Tests.Helpers;

namespace Mithril.Features.Tests.Admin
{
    /// <summary>
    /// Feature editor tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;FeatureEditor&gt;" />
    public class FeatureEditorTests : TestBaseClass<FeatureEditor>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureEditorTests"/> class.
        /// </summary>
        public FeatureEditorTests()
        {
            TestObject = new FeatureEditor(null, null, null);
            ObjectType = typeof(FeatureEditor);
        }
    }
}