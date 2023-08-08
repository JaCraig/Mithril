using Mithril.Features.Commands.ViewModels;
using Mithril.Tests.Helpers;

namespace Mithril.Features.Tests.Commands.ViewModels
{
    /// <summary>
    /// Toggle feature command VM tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;ToggleFeatureCommandVM&gt;"/>
    public class ToggleFeatureCommandVMTests : TestBaseClass<ToggleFeatureCommandVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToggleFeatureCommandVMTests"/> class.
        /// </summary>
        public ToggleFeatureCommandVMTests()
        {
            TestObject = new ToggleFeatureCommandVM();
        }
    }
}