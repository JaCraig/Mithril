using Mithril.Core.Abstractions.Modules.BaseClasses;

namespace Mithril.Core.Abstractions.Modules.Features
{
    /// <summary>
    /// Generic feature (can be used for adhoc situations but should be done so sparingly)
    /// </summary>
    /// <seealso cref="FeatureBaseClass&lt;GenericFeature&gt;"/>
    public class GenericFeature : FeatureBaseClass<GenericFeature>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericFeature"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="category">The category.</param>
        /// <param name="description">The description.</param>
        public GenericFeature(string name, string category, string description)
        {
            Name = name;
            Category = category;
            Description = description;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericFeature"/> class.
        /// </summary>
        public GenericFeature()
            : this("Generic", "", "Generic feature")
        {
        }

        /// <summary>
        /// The group (by category) that the feature belongs. If not provided, defaults to 'Uncategorized'.
        /// </summary>
        /// <value>The category.</value>
        public override string Category { get; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public override string Description { get; }

        /// <summary>
        /// Human-readable name of the feature. If not provided, the identifier will be used.
        /// </summary>
        /// <value>The name.</value>
        public override string Name { get; }
    }
}