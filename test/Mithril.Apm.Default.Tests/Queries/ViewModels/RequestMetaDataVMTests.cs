using Mithril.Apm.Default.Queries.ViewModels;
using Mithril.Tests.Helpers;

namespace Mithril.Apm.Default.Tests.Queries.ViewModels
{
    /// <summary>
    /// RequestMetaDataVM tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;RequestMetaDataVM&gt;"/>
    public class RequestMetaDataVMTests : TestBaseClass<RequestMetaDataVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestMetaDataVMTests"/> class.
        /// </summary>
        public RequestMetaDataVMTests()
        {
            TestObject = new RequestMetaDataVM(null);
            ObjectType = typeof(RequestMetaDataVM);
        }
    }
}