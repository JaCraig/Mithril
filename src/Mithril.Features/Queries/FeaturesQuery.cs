using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Mithril.API.Abstractions.Attributes;
using Mithril.API.Abstractions.Query;
using Mithril.API.Abstractions.Query.BaseClasses;
using Mithril.Data.Abstractions.Services;
using Mithril.Features.Models;
using System.Security.Claims;

namespace Mithril.Features.Queries
{
    /// <summary>
    /// Features query
    /// </summary>
    [ApiAuthorize("Admin Only")]
    public class FeaturesQuery : QueryBaseClass<IEnumerable<FeatureVM>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeaturesQuery"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="featureManager">The feature manager.</param>
        /// <param name="dataService">The data service.</param>
        public FeaturesQuery(ILogger<FeaturesQuery>? logger, IFeatureManager? featureManager, IDataService? dataService)
            : base(logger, featureManager)
        {
            DataService = dataService;
        }

        /// <summary>
        /// Gets the data service.
        /// </summary>
        /// <value>The data service.</value>
        public IDataService? DataService { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public override string Name => "Features";

        /// <summary>
        /// Resolves the asynchronous.
        /// </summary>
        /// <param name="arg">The argument.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns></returns>
        public override Task<IEnumerable<FeatureVM>?> ResolveAsync(ClaimsPrincipal? arg, Arguments arguments) => Task.FromResult<IEnumerable<FeatureVM>?>(Feature.All(DataService).Select(x => new FeatureVM(x)));
    }
}