using Mithril.Admin.Abstractions.DataEditor.Attributes;
using Mithril.Tests.Helpers;

namespace Mithril.Admin.Abstractions.Tests.DataEditor.Attributes
{
    /// <summary>
    /// Date and time attribute tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;DateAndTimeAttribute&gt;" />
    public class DateAndTimeAttributeTests : TestBaseClass<DateAndTimeAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateAndTimeAttributeTests"/> class.
        /// </summary>
        public DateAndTimeAttributeTests()
        {
            TestObject = new DateAndTimeAttribute();
        }
    }
}