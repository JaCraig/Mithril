using BigBook;
using Microsoft.FeatureManagement;
using Mithril.Core.Abstractions.Modules.Interfaces;

namespace Mithril.Core.Abstractions.Extensions
{
    /// <summary>
    /// Feature manager extension
    /// </summary>
    public static class IFeatureManagerExtensions
    {
        /// <summary>
        /// Determines if the features are enabled.
        /// </summary>
        /// <param name="featureManager">The feature manager.</param>
        /// <param name="features">The features.</param>
        /// <returns>True if they are, false otherwise.</returns>
        public static bool AreFeaturesEnabled(this IFeatureManager? featureManager, params IFeature[] features)
        {
            return featureManager is null
                || features is null
                || features.Length == 0
                || features.All(x => AsyncHelper.RunSync(() => featureManager.IsEnabledAsync(x?.Name ?? "")));
        }
    }
}