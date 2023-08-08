using Mithril.Admin.Abstractions.DataEditor.Attributes;
using Mithril.Tests.Helpers;

namespace Mithril.Admin.Abstractions.Tests.DataEditor.Attributes
{
    /// <summary>
    /// Pattern attribute tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;PatternAttribute&gt;" />
    public class PatternAttributeTests : TestBaseClass<PatternAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PatternAttributeTests"/> class.
        /// </summary>
        public PatternAttributeTests()
        {
            TestObject = new PatternAttribute("test");
            ObjectType = typeof(PatternAttribute);
        }
    }
}