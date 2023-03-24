using Mithril.Tests.Helpers;
using Mithril.Themes.Resources;

namespace Mithril.Themes.Tests.Resources
{
    /// <summary>
    /// ScriptFileResource tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;ScriptFileResource&gt;" />
    public class ScriptFileResourceTests : TestBaseClass<ScriptFileResource>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptFileResourceTests"/> class.
        /// </summary>
        public ScriptFileResourceTests()
        {
            TestObject = new ScriptFileResource("", "", "", "", "", "", 0, "");
            ObjectType = typeof(ScriptFileResource);
        }
    }
}