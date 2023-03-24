using Mithril.Tests.Helpers;
using Mithril.Themes.Models;

namespace Mithril.Themes.Tests.Models
{
    /// <summary>
    /// ThemeChangedEvent tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;ThemeChangedEvent&gt;" />
    public class ThemeChangedEventTests : TestBaseClass<ThemeChangedEvent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThemeChangedEventTests"/> class.
        /// </summary>
        public ThemeChangedEventTests()
        {
            TestObject = new ThemeChangedEvent("Test");
            ObjectType = typeof(ThemeChangedEvent);
        }
    }
}