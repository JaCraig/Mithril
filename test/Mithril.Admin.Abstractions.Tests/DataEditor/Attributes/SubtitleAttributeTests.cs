using Mithril.Admin.Abstractions.DataEditor.Attributes;
using Mithril.Tests.Helpers;

namespace Mithril.Admin.Abstractions.Tests.DataEditor.Attributes
{
    /// <summary>
    /// Subtitle attribute tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;SubtitleAttribute&gt;" />
    public class SubtitleAttributeTests : TestBaseClass<SubtitleAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubtitleAttributeTests"/> class.
        /// </summary>
        public SubtitleAttributeTests()
        {
            TestObject = new SubtitleAttribute("test");
            ObjectType = typeof(SubtitleAttribute);
        }
    }
}