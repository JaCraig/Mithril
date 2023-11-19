using Microsoft.FeatureManagement;
using Mithril.Core.Abstractions.Extensions;
using Mithril.Data.Abstractions.Services;
using Mithril.Navigation.Abstractions.Interfaces;
using Mithril.Navigation.Abstractions.Services;
using Mithril.Navigation.Features;
using System.Security.Claims;

namespace Mithril.Navigation.Services
{
    /// <summary>
    /// Menu service
    /// </summary>
    /// <seealso cref="IMenuService" />
    /// <remarks>
    /// Initializes a new instance of the <see cref="MenuService" /> class.
    /// </remarks>
    /// <param name="featureManager">The feature manager.</param>
    /// <param name="dataService">The data service.</param>
    public class MenuService(IFeatureManager? featureManager, IDataService? dataService) : IMenuService
    {
        /// <summary>
        /// Gets the data service.
        /// </summary>
        /// <value>
        /// The data service.
        /// </value>
        private IDataService? DataService { get; } = dataService;

        /// <summary>
        /// Gets the feature manager.
        /// </summary>
        /// <value>
        /// The feature manager.
        /// </value>
        private IFeatureManager? FeatureManager { get; } = featureManager;

        /// <summary>
        /// Creates a menu builder.
        /// </summary>
        /// <param name="display">The display name of the menu.</param>
        /// <param name="user">The user.</param>
        /// <returns>
        /// The menu builder.
        /// </returns>
        public IMenuBuilder? CreateMenuBuilder(string display, ClaimsPrincipal? user) => FeatureManager.AreFeaturesEnabled(NavigationFeature.Instance) ? null : (IMenuBuilder)new MenuBuilder(display, DataService, user);
    }
}