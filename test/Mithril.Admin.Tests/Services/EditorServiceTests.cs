using Mithril.Admin.Abstractions.Interfaces;
using Mithril.Admin.Services;
using Mithril.Tests.Helpers;

namespace Mithril.Admin.Tests.Services
{
    /// <summary>
    /// Editor service tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;EditorService&gt;" />
    public class EditorServiceTests : TestBaseClass<EditorService>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditorServiceTests"/> class.
        /// </summary>
        public EditorServiceTests()
        {
            TestObject = new EditorService(Array.Empty<IEditor>());
            ObjectType = typeof(EditorService);
        }
    }
}