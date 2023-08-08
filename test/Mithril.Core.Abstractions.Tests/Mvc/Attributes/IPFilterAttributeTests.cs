using Mithril.Core.Abstractions.Mvc.Attributes;
using Mithril.Tests.Helpers;

namespace Mithril.Core.Abstractions.Tests.Mvc.Attributes
{
    public class IPFilterAttributeTests : TestBaseClass<IPFilterAttribute>
    {
        public IPFilterAttributeTests()
        {
            TestObject = new IPFilterAttribute("Default");
            ObjectType = typeof(IPFilterAttribute);
        }
    }
}