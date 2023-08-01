using Mithril.Admin.Abstractions.DataEditor.Attributes;
using Mithril.Tests.Helpers;

namespace Mithril.Admin.Abstractions.Tests.DataEditor.Attributes
{
    /// <summary>
    /// Order attribute tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;OrderAttribute&gt;" />
    public class OrderAttributeTests : TestBaseClass<OrderAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderAttributeTests"/> class.
        /// </summary>
        public OrderAttributeTests()
        {
            TestObject = new OrderAttribute(1);
            ObjectType = typeof(OrderAttribute);
        }
    }
}