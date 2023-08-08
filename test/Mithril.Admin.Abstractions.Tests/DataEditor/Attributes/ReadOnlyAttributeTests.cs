using Mithril.Admin.Abstractions.DataEditor.Attributes;
using Mithril.Tests.Helpers;

namespace Mithril.Admin.Abstractions.Tests.DataEditor.Attributes
{
    /// <summary>
    /// Read only attribute tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;ReadOnlyAttribute&gt;" />
    public class ReadOnlyAttributeTests : TestBaseClass<ReadOnlyAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyAttributeTests"/> class.
        /// </summary>
        public ReadOnlyAttributeTests()
        {
            TestObject = new ReadOnlyAttribute();
        }
    }
}