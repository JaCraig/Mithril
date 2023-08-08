using Mithril.Admin.Abstractions.DataEditor.Attributes;
using Mithril.Tests.Helpers;

namespace Mithril.Admin.Abstractions.Tests.DataEditor.Attributes
{
    /// <summary>
    /// Text area attribute tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;TextAreaAttribute&gt;" />
    public class TextAreaAttributeTests : TestBaseClass<TextAreaAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextAreaAttributeTests"/> class.
        /// </summary>
        public TextAreaAttributeTests()
        {
            TestObject = new TextAreaAttribute();
        }
    }
}