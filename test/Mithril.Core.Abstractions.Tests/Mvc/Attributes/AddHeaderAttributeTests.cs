using Mithril.Core.Abstractions.Mvc.Attributes;
using Mithril.Tests.Helpers;

namespace Mithril.Core.Abstractions.Tests.Mvc.Attributes
{
    /// <summary>
    /// Add Header Attribute Tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;AddHeaderAttribute&gt;" />
    public class AddHeaderAttributeTests : TestBaseClass<AddHeaderAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddHeaderAttributeTests"/> class.
        /// </summary>
        public AddHeaderAttributeTests()
        {
            TestObject = new AddHeaderAttribute("Key", "Value");
            ObjectType = typeof(AddHeaderAttribute);
        }
    }
}