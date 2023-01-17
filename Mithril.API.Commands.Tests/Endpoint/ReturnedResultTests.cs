using Mithril.API.Commands.Endpoint;
using Mithril.Tests.Helpers;

namespace Mithril.API.Commands.Tests.Endpoint
{
    /// <summary>
    /// Returned result tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;ReturnedResult&gt;"/>
    public class ReturnedResultTests : TestBaseClass<ReturnedResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnedResultTests"/> class.
        /// </summary>
        public ReturnedResultTests()
        {
            TestObject = new ReturnedResult();
        }
    }
}