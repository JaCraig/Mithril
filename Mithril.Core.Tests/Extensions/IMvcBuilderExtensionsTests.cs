using Microsoft.Extensions.DependencyInjection;
using Mithril.Tests.Helpers;

namespace Mithril.Core.Tests.Extensions
{
    public class IMvcBuilderExtensionsTests : TestBaseClass
    {
        protected override Type? ObjectType { get; set; } = typeof(IMvcBuilderExtensions);
    }
}