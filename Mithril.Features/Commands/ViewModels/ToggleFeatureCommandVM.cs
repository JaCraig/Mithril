namespace Mithril.Features.Commands.ViewModels
{
    /// <summary>
    /// Toggle feature command vm
    /// </summary>
    public class ToggleFeatureCommandVM
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ToggleFeatureCommandVM"/> is active.
        /// </summary>
        /// <value><c>true</c> if active; otherwise, <c>false</c>.</value>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string? FeatureName { get; set; }
    }
}