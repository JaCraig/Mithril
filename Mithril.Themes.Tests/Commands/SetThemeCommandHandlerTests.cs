using Mithril.Tests.Helpers;
using Mithril.Themes.Abstractions.Interfaces;
using Mithril.Themes.Commands;

namespace Mithril.Themes.Tests.Commands
{
    /// <summary>
    /// SetThemeCommandHandler tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;SetThemeCommandHandler&gt;" />
    public class SetThemeCommandHandlerTests : TestBaseClass<SetThemeCommandHandler>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetThemeCommandHandlerTests"/> class.
        /// </summary>
        public SetThemeCommandHandlerTests()
        {
            TestObject = new SetThemeCommandHandler(null, null, null, null, Array.Empty<ITheme>());
            ObjectType = typeof(SetThemeCommandHandler);
        }
    }
}