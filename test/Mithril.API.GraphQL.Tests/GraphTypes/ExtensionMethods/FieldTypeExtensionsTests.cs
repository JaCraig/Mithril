using Mithril.API.GraphQL.GraphTypes.ExtensionMethods;
using Mithril.Tests.Helpers;

namespace Mithril.API.GraphQL.Tests.GraphTypes.ExtensionMethods
{
    /// <summary>
    /// Field type extensions
    /// </summary>
    /// <seealso cref="Mithril.Tests.Helpers.TestBaseClass"/>
    public class FieldTypeExtensionsTests : TestBaseClass
    {
        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        protected override Type? ObjectType { get; set; } = typeof(FieldTypeExtensions);
    }
}