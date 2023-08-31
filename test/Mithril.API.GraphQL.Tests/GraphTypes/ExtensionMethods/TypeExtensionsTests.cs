using Mithril.API.GraphQL.GraphTypes.ExtensionMethods;
using Mithril.Tests.Helpers;

namespace Mithril.API.GraphQL.Tests.GraphTypes.ExtensionMethods
{
    public class TypeExtensionsTests : TestBaseClass
    {
        protected override Type? ObjectType { get; set; } = typeof(TypeExtensions);
    }
}