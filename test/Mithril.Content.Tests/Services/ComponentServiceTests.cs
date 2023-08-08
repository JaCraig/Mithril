using Mithril.Content.Abstractions.Interfaces;
using Mithril.Content.Services;
using Mithril.Tests.Helpers;

namespace Mithril.Content.Tests.Services
{
    /// <summary>
    /// ComponentService tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;ComponentService&gt;" />
    public class ComponentServiceTests : TestBaseClass<ComponentService>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentServiceTests"/> class.
        /// </summary>
        public ComponentServiceTests()
        {
            TestObject = new ComponentService(Array.Empty<IComponentDefinition>());
            ObjectType = typeof(ComponentService);
        }
    }
}