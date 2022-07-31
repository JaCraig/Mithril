namespace Mithril.Core.Abstractions.Modules.Interfaces
{
    /// <summary>
    /// Feature interface. Defines information about the feature.
    /// </summary>
    public interface IFeature
    {
        /// <summary>
        /// The group (by category) that the feature belongs. If not provided, defaults to 'Uncategorized'.
        /// </summary>
        /// <value>The category.</value>
        string Category { get; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        string Description { get; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        string Id { get; }

        /// <summary>
        /// Human-readable name of the feature. If not provided, the identifier will be used.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }
    }
}