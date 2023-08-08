using Mithril.Tests.Helpers;
using Mithril.Themes.Abstractions.TagHelpers;

namespace Mithril.Themes.Abstractions.Tests.TagHelpers
{
    /// <summary>
    /// ScriptTagHelper tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;ScriptTagHelper&gt;" />
    public class ScriptTagHelperTests : TestBaseClass<ScriptTagHelper>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptTagHelperTests"/> class.
        /// </summary>
        public ScriptTagHelperTests()
        {
            TestObject = new ScriptTagHelper(null);
            ObjectType = typeof(ScriptTagHelper);
        }
    }
}