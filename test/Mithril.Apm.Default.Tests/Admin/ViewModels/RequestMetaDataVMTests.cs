using Mithril.Apm.Default.Admin.ViewModels;
using Mithril.Tests.Helpers;

namespace Mithril.Apm.Default.Tests.Admin.ViewModels
{
    /// <summary>
    /// Request meta data VM tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;RequestMetaDataVM&gt;" />
    public class RequestMetaDataVMTests : TestBaseClass<RequestMetaDataVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestMetaDataVMTests"/> class.
        /// </summary>
        public RequestMetaDataVMTests()
        {
            TestObject = new RequestMetaDataVM();
            ObjectType = typeof(RequestMetaDataVM);
        }
    }
}