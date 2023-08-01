using Mithril.Tests.Helpers;
using Mithril.Themes.Admin.ViewModels;

namespace Mithril.Themes.Tests.Admin.ViewModels
{
    /// <summary>
    /// Theme Settings VM Tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;ThemeSettingsVM&gt;" />
    public class ThemeSettingsVMTests : TestBaseClass<ThemeSettingsVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThemeSettingsVMTests"/> class.
        /// </summary>
        public ThemeSettingsVMTests()
        {
            TestObject = new ThemeSettingsVM(null, null);
            ObjectType = typeof(ThemeSettingsVM);
        }
    }
}