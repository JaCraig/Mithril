using Mithril.Features.Models;

namespace Mithril.Features.Queries
{
    /// <summary>
    /// Feature List VM
    /// </summary>
    public class FeatureListVM
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureListVM"/> class.
        /// </summary>
        /// <param name="features">The features.</param>
        public FeatureListVM(IEnumerable<Feature> features)
        {
            Features.AddRange(features.Select(x => new FeatureVM(x)));
        }

        /// <summary>
        /// Gets the features.
        /// </summary>
        /// <value>The features.</value>
        public List<FeatureVM> Features { get; } = [];
    }
}