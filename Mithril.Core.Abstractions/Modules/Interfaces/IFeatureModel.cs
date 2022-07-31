using Mithril.Core.Abstractions.Data.Interfaces;

namespace Mithril.Core.Abstractions.Modules.Interfaces
{
    /// <summary>
    /// Feature model interface
    /// </summary>
    /// <seealso cref="IModel"/>
    public interface IFeatureModel : IModel
    {
        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>The category.</value>
        string? Category { get; set; }

        /// <summary>
        /// Gets or sets the dependencies.
        /// </summary>
        /// <value>The dependencies.</value>
        IList<IFeatureModel> Dependencies { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        string? Description { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        string? DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        string? Identifier { get; set; }
    }
}