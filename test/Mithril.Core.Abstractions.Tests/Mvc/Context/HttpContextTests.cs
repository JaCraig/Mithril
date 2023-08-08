using Mithril.Core.Abstractions.Mvc.Context;
using Mithril.Tests.Helpers;

namespace Mithril.Core.Abstractions.Tests.Mvc.Context
{
    public class HttpContextTests : TestBaseClass
    {
        protected override Type? ObjectType { get; set; } = typeof(HttpContext);
    }
}