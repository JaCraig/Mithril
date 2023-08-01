using Mithril.Admin.Abstractions.DataEditor.Attributes;
using Mithril.Tests.Helpers;

namespace Mithril.Admin.Abstractions.Tests.DataEditor.Attributes
{
    /// <summary>
    /// Input type attribute tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;InputTypeAttribute&gt;" />
    public class InputTypeAttributeTests : TestBaseClass<InputTypeAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InputTypeAttributeTests"/> class.
        /// </summary>
        public InputTypeAttributeTests()
        {
            TestObject = new InputTypeAttribute("date");
            ObjectType = typeof(InputTypeAttribute);
        }
    }
}