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
    /// <seealso cref="QueryBaseClass&lt;IEnumerable&lt;FeatureVM&gt;&gt;"/>
    public class FeaturesQuery : QueryBaseClass<IEnumerable<FeatureVM>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeaturesQuery"/> class.
        /// </summary>
        /// <param name="dataService">The data service.</param>
        public FeaturesQuery(IDataService dataService)
        {
            DataService = dataService;
        }

        /// <summary>
        /// Gets the data service.
        /// </summary>
        /// <value>The data service.</value>
        public IDataService DataService { get; }

        /// <summary>
        /// Resolves the asynchronous.
        /// </summary>
        /// <param name="arg">The argument.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns></returns>
        public override Task<IEnumerable<FeatureVM>?> ResolveAsync(ClaimsPrincipal? arg, Arguments arguments)
        {
            if (!(arg?.HasClaim("Role", "Admin") ?? false))
                return Task.FromResult<IEnumerable<FeatureVM>?>(Array.Empty<FeatureVM>());
            return Task.FromResult<IEnumerable<FeatureVM>?>(Feature.All(DataService).Select(x => new FeatureVM(x)));
        }
    }
}