using Mithril.Core.Abstractions.Modules.BaseClasses;

namespace Mithril.Logging.Features
{
    /// <summary>
    /// Logging feature
    /// </summary>
    /// <seealso cref="FeatureBaseClass{LoggingFeature}"/>
    public class LoggingFeature : FeatureBaseClass<LoggingFeature>
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
        public override string Description { get; } = "Allows logging through the API/system";

        /// <summary>
        /// Human-readable name of the feature. If not provided, the identifier will be used.
        /// </summary>
        /// <value>The name.</value>
        public override string Name => nameof(LoggingFeature);
    }
}