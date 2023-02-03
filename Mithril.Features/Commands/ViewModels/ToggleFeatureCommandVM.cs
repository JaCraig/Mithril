namespace Mithril.Features.Commands.ViewModels
{
    /// <summary>
    /// Toggle feature command, used to turn on/off sections of the application as needed. Requires
    /// admin access.
    /// </summary>
    public class ToggleFeatureCommandVM
    {
        /// <summary>
        /// Gets or sets a value indicating whether the feature should be made active or not.
        /// </summary>
        /// <value><c>true</c> if active; otherwise, <c>false</c>.</value>
        public bool Active { get; set; }

        /// <summary>
        /// The name of the feature to toggle.
        /// </summary>
        /// <value>The name.</value>
        public string? FeatureName { get; set; }
    }
}