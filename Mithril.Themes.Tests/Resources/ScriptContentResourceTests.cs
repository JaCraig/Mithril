using Mithril.Tests.Helpers;
using Mithril.Themes.Resources;

namespace Mithril.Themes.Tests.Resources
{
    /// <summary>
    /// ScriptContentResource tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;ScriptContentResource&gt;" />
    public class ScriptContentResourceTests : TestBaseClass<ScriptContentResource>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptContentResourceTests"/> class.
        /// </summary>
        public ScriptContentResourceTests()
        {
            TestObject = new ScriptContentResource("", "", "", "", "", "", 0, "");
            ObjectType = typeof(ScriptContentResource);
        }
    }
}