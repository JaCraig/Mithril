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
    public class MenuService : IMenuService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuService" /> class.
        /// </summary>
        /// <param name="featureManager">The feature manager.</param>
        /// <param name="dataService">The data service.</param>
        public MenuService(IFeatureManager? featureManager, IDataService? dataService)
        {
            FeatureManager = featureManager;
            DataService = dataService;
        }

        /// <summary>
        /// Gets the data service.
        /// </summary>
        /// <value>
        /// The data service.
        /// </value>
        private IDataService? DataService { get; }

        /// <summary>
        /// Gets the feature manager.
        /// </summary>
        /// <value>
        /// The feature manager.
        /// </value>
        private IFeatureManager? FeatureManager { get; }

        /// <summary>
        /// Creates a menu builder.
        /// </summary>
        /// <param name="display">The display name of the menu.</param>
        /// <param name="user">The user.</param>
        /// <returns>
        /// The menu builder.
        /// </returns>
        public IMenuBuilder? CreateMenuBuilder(string display, ClaimsPrincipal? user)
        {
            if (FeatureManager.AreFeaturesEnabled(NavigationFeature.Instance))
                return null;
            return new MenuBuilder(display, DataService, user);
        }
    }
}