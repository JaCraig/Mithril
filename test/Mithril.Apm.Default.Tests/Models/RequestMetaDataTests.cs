using Mithril.Apm.Default.Models;
using Mithril.Tests.Helpers;

namespace Mithril.Apm.Default.Tests.Models
{
    /// <summary>
    /// Request meta data tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;RequestMetaData&gt;"/>
    public class RequestMetaDataTests : TestBaseClass<RequestMetaData>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestMetaDataTests"/> class.
        /// </summary>
        public RequestMetaDataTests()
        {
            TestObject = new RequestMetaData();
            ObjectType = typeof(RequestMetaData);
        }
    }
}