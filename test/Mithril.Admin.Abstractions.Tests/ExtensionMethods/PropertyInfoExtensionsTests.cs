using Mithril.Admin.Abstractions.ExtensionMethods;
using Mithril.Tests.Helpers;

namespace Mithril.Admin.Abstractions.Tests.ExtensionMethods
{
    /// <summary>
    /// Property info extensions tests
    /// </summary>
    /// <seealso cref="TestBaseClass" />
    public class PropertyInfoExtensionsTests : TestBaseClass
    {
        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        /// <value>
        /// The type of the object.
        /// </value>
        protected override Type? ObjectType { get; set; } = typeof(PropertyInfoExtensions);
    }
}