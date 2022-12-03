using Mithril.Core.Abstractions.Modules.Features;
using Mithril.Tests.Helpers;

namespace Mithril.Core.Abstractions.Tests.Modules.Features
{
    /// <summary>
    /// Generic feature tests
    /// </summary>
    /// <seealso cref="Mithril.Tests.Helpers.TestBaseClass"/>
    public class GenericFeatureTests : TestBaseClass
    {
        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        protected override Type? ObjectType { get; set; } = typeof(GenericFeature);
    }
}