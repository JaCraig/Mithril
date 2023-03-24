using Mithril.Tests.Helpers;
using Mithril.Themes.Models;

namespace Mithril.Themes.Tests.Models
{
    /// <summary>
    /// SetThemeCommand tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;SetThemeCommand&gt;" />
    public class SetThemeCommandTests : TestBaseClass<SetThemeCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetThemeCommandTests"/> class.
        /// </summary>
        public SetThemeCommandTests()
        {
            TestObject = new SetThemeCommand("Test");
            ObjectType = typeof(SetThemeCommand);
        }
    }
}