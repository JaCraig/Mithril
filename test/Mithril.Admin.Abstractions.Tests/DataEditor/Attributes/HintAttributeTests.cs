using Mithril.Admin.Abstractions.DataEditor.Attributes;
using Mithril.Tests.Helpers;

namespace Mithril.Admin.Abstractions.Tests.DataEditor.Attributes
{
    /// <summary>
    /// Hint attribute tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;HintAttribute&gt;" />
    public class HintAttributeTests : TestBaseClass<HintAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HintAttributeTests"/> class.
        /// </summary>
        public HintAttributeTests()
        {
            TestObject = new HintAttribute("hint");
            ObjectType = typeof(HintAttribute);
        }
    }
}