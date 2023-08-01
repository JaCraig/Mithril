using Mithril.API.Abstractions.Query.ViewModels;
using Mithril.Tests.Helpers;

namespace Mithril.API.Abstractions.Tests.Query.ViewModels
{
    /// <summary>
    /// Example class
    /// </summary>
    public class ExampleClass
    { }

    /// <summary>
    /// Paged data view model tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;PagedDataVM&lt;ExampleClass&gt;&gt;" />
    public class PagedDataVMTests : TestBaseClass<PagedDataVM<ExampleClass>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PagedDataVMTests"/> class.
        /// </summary>
        public PagedDataVMTests()
        {
            TestObject = new PagedDataVM<ExampleClass>(0, 10, 100, 1, new List<ExampleClass>());
            ObjectType = typeof(PagedDataVM<ExampleClass>);
        }
    }
}