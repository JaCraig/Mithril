using Mithril.Core.Abstractions.Modules.BaseClasses;

namespace Mithril.Core.Abstractions.Modules.Features
{
    /// <summary>
    /// Media feature
    /// </summary>
    /// <seealso cref="FeatureBaseClass{MediaFeature}"/>
    public class MediaFeature : FeatureBaseClass<MediaFeature>
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
        public override string Description { get; } = "Allows file uploads through the API";

        /// <summary>
        /// Human-readable name of the feature. If not provided, the identifier will be used.
        /// </summary>
        /// <value>The name.</value>
        public override string Name { get; } = "Media";
    }
}