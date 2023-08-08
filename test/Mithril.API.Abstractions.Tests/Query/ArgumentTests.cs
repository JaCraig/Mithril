using Mithril.API.Abstractions.Query;
using Mithril.Tests.Helpers;

namespace Mithril.API.Abstractions.Tests.Query
{
    public class ArgumentTests : TestBaseClass<Argument<string>>
    {
        public ArgumentTests()
        {
            TestObject = new Argument<string>();
        }
    }
}