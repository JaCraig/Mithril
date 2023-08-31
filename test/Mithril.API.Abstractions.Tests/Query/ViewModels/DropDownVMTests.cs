using Mithril.API.Abstractions.Query.ViewModels;
using Mithril.Tests.Helpers;

namespace Mithril.API.Abstractions.Tests.Query.ViewModels
{
    /// <summary>
    /// Drop down VM tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;DropDownVM&lt;long&gt;&gt;" />
    public class DropDownVMTests : TestBaseClass<DropDownVM<long>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DropDownVMTests"/> class.
        /// </summary>
        public DropDownVMTests()
        {
            TestObject = new DropDownVM<long>(1, "test");
            ObjectType = typeof(DropDownVM<long>);
        }
    }
}