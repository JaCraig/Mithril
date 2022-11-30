using Mithril.Features.Models;

namespace Mithril.Features.Queries
{
    /// <summary>
    /// Feature view model
    /// </summary>
    public class FeatureVM
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureVM"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public FeatureVM(Feature model)
        {
            if (model is null)
                return;
            Name = model.Name;
            Active = model.Active;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="FeatureVM"/> is active.
        /// </summary>
        /// <value><c>true</c> if active; otherwise, <c>false</c>.</value>
        public bool Active { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string? Name { get; }
    }
}