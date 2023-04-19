using Mithril.Core.Abstractions.Extensions;
using Mithril.Tests.Helpers;

namespace Mithril.Core.Abstractions.Tests.Extensions
{
    /// <summary>
    /// IServiceProviderExtensions tests
    /// </summary>
    /// <seealso cref="TestBaseClass" />
    public class IServiceProviderExtensionsTests : TestBaseClass
    {
        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        /// <value>
        /// The type of the object.
        /// </value>
        protected override Type? ObjectType { get; set; } = typeof(IServiceProviderExtensions);
    }
}