using Mithril.Admin.Abstractions.Events;
using Mithril.Tests.Helpers;

namespace Mithril.Admin.Abstractions.Tests.Events
{
    /// <summary>
    /// Model saved event tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;ModelSavedEvent&gt;" />
    public class ModelSavedEventTests : TestBaseClass<ModelSavedEvent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelSavedEventTests"/> class.
        /// </summary>
        public ModelSavedEventTests()
        {
            TestObject = new ModelSavedEvent();
        }
    }
}