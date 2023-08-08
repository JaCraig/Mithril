using Mithril.Admin.Abstractions.DataEditor.Attributes;
using Mithril.Tests.Helpers;

namespace Mithril.Admin.Abstractions.Tests.DataEditor.Attributes
{
    /// <summary>
    /// Password attribute tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;PasswordAttribute&gt;" />
    public class PasswordAttributeTests : TestBaseClass<PasswordAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordAttributeTests"/> class.
        /// </summary>
        public PasswordAttributeTests()
        {
            TestObject = new PasswordAttribute();
        }
    }
}