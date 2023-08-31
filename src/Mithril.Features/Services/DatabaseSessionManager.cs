using Microsoft.FeatureManagement;
using Mithril.Core.Abstractions.Mvc.Context;
using Mithril.Data.Abstractions.Services;
using Mithril.Features.Models;

namespace Mithril.Features.Services
{
    /// <summary>
    /// Database session manager
    /// </summary>
    /// <seealso cref="ISessionManager"/>
    public class DatabaseSessionManager : ISessionManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseSessionManager"/> class.
        /// </summary>
        /// <param name="dataService">The data service.</param>
        public DatabaseSessionManager(IDataService? dataService)
        {
            DataService = dataService;
        }

        /// <summary>
        /// Gets the data service.
        /// </summary>
        /// <value>The data service.</value>
        public IDataService? DataService { get; }

        /// <summary>
        /// Queries the session manager for the session's feature state, if any, for the given feature.
        /// </summary>
        /// <param name="featureName">The name of the feature.</param>
        /// <returns>The state of the feature if it is present in the session, otherwise null.</returns>
        public Task<bool?> GetAsync(string featureName) => Task.FromResult(Feature.Load(featureName, DataService)?.Active);

        /// <summary>
        /// Set the state of a feature to be used for a session.
        /// </summary>
        /// <param name="featureName">The name of the feature.</param>
        /// <param name="enabled">The state of the feature.</param>
        /// <returns>The async task.</returns>
        public Task SetAsync(string featureName, bool enabled)
        {
            var TempFeature = Feature.Load(featureName, DataService);
            if (TempFeature is null)
                return Task.CompletedTask;
            TempFeature.Active = enabled;
            return TempFeature.SaveAsync(DataService, HttpContext.Current?.User);
        }
    }
}