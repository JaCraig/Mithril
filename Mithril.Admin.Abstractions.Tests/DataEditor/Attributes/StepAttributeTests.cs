using Mithril.Admin.Abstractions.DataEditor.Attributes;
using Mithril.Tests.Helpers;

namespace Mithril.Admin.Abstractions.Tests.DataEditor.Attributes
{
    /// <summary>
    /// Step attribute tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;StepAttribute&gt;" />
    public class StepAttributeTests : TestBaseClass<StepAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StepAttributeTests"/> class.
        /// </summary>
        public StepAttributeTests()
        {
            TestObject = new StepAttribute(1);
            ObjectType = typeof(StepAttribute);
        }
    }
}