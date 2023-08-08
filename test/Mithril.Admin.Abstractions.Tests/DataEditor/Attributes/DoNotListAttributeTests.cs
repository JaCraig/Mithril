using Mithril.Admin.Abstractions.DataEditor.Attributes;
using Mithril.Tests.Helpers;

namespace Mithril.Admin.Abstractions.Tests.DataEditor.Attributes
{
    /// <summary>
    /// Do not list attribute tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;DoNotListAttribute&gt;" />
    public class DoNotListAttributeTests : TestBaseClass<DoNotListAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DoNotListAttributeTests"/> class.
        /// </summary>
        public DoNotListAttributeTests()
        {
            TestObject = new DoNotListAttribute();
        }
    }
}