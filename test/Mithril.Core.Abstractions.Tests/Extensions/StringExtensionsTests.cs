using Mithril.Core.Abstractions.Extensions;
using Mithril.Tests.Helpers;

namespace Mithril.Core.Abstractions.Tests.Extensions
{
    /// <summary>
    /// String extension methods
    /// </summary>
    /// <seealso cref="TestBaseClass" />
    public class StringExtensionsTests : TestBaseClass
    {
        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        /// <value>
        /// The type of the object.
        /// </value>
        protected override Type? ObjectType { get; set; } = typeof(StringExtensions);
    }
}