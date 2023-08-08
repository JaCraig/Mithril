using Mithril.Tests.Helpers;
using Mithril.Themes.Commands;

namespace Mithril.Themes.Tests.Commands
{
    /// <summary>
    /// ThemeChangedEventDefaultHandler tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;ThemeChangedEventDefaultHandler&gt;" />
    public class ThemeChangedEventDefaultHandlerTests : TestBaseClass<ThemeChangedEventDefaultHandler>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThemeChangedEventDefaultHandlerTests"/> class.
        /// </summary>
        public ThemeChangedEventDefaultHandlerTests()
        {
            TestObject = new ThemeChangedEventDefaultHandler(null, null);
            ObjectType = typeof(ThemeChangedEventDefaultHandler);
        }
    }
}