using Mithril.Core.Abstractions.Modules.BaseClasses;

namespace Mithril.Apm.Abstractions.Features
{
    /// <summary>
    /// APM Feature
    /// </summary>
    /// <seealso cref="FeatureBaseClass&lt;APMFeature&gt;"/>
    public class APMFeature : FeatureBaseClass<APMFeature>
    {
        /// <summary>
        /// The group (by category) that the feature belongs. If not provided, defaults to 'Uncategorized'.
        /// </summary>
        /// <value>The category.</value>
        public override string Category { get; } = "Core";

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public override string Description { get; } = "Allows APM services to run";

        /// <summary>
        /// Human-readable name of the feature. If not provided, the identifier will be used.
        /// </summary>
        /// <value>The name.</value>
        public override string Name => nameof(APMFeature);
    }
}