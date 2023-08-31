using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Mithril.API.Abstractions.Attributes;
using Mithril.API.Abstractions.Query;
using Mithril.API.Abstractions.Query.BaseClasses;
using Mithril.API.Abstractions.Query.Interfaces;
using Mithril.Apm.Abstractions.Features;
using Mithril.Apm.Default.Models;
using Mithril.Apm.Default.Queries.ViewModels;
using Mithril.Core.Abstractions.Modules.Interfaces;
using Mithril.Data.Abstractions.Services;
using System.Security.Claims;

namespace Mithril.Apm.Default.Queries
{
    /// <summary>
    /// Request trace query
    /// </summary>
    [ApiAuthorize("Admin Only")]
    public class RequestTraceQuery : QueryBaseClass<IEnumerable<RequestTraceVM>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestTraceQuery"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="featureManager">The feature manager.</param>
        /// <param name="dataService">The data service.</param>
        public RequestTraceQuery(ILogger<RequestTraceQuery>? logger, IFeatureManager? featureManager, IDataService? dataService)
            : base(logger, featureManager)
        {
            DataService = dataService;
        }

        /// <summary>
        /// The arguments
        /// </summary>
        public override IArgument[] Arguments { get; } = new IArgument[]
        {
            new Argument<DateTime>{ DefaultValue = DateTime.Today.AddHours(DateTime.Now.Hour), Description = "Start date/time", Name = "start"},
            new Argument<DateTime>{ DefaultValue = DateTime.Today.AddHours(DateTime.Now.AddHours(1).Hour), Description = "End date/time",Name = "end"}
        };

        /// <summary>
        /// Gets the data service.
        /// </summary>
        /// <value>The data service.</value>
        public IDataService? DataService { get; }

        /// <summary>
        /// Gets the features associated with this command.
        /// </summary>
        /// <value>The features associated with this command.</value>
        public override IFeature[] Features { get; } = new IFeature[]
        {
            APMFeature.Instance
        };

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public override string Name => "RequestTrace";

        /// <summary>
        /// Resolves the asynchronous.
        /// </summary>
        /// <param name="arg">The argument.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns></returns>
        public override Task<IEnumerable<RequestTraceVM>?> ResolveAsync(ClaimsPrincipal? arg, Arguments arguments)
        {
            arguments ??= new Arguments();
            DateTime Start = arguments.GetValue<DateTime>("start");
            DateTime End = arguments.GetValue<DateTime>("end");

            return Task.FromResult(RequestTrace.Query(DataService)?.Where(x => x.DateCreated >= Start && x.DateCreated <= End).ToList().Select(x => new RequestTraceVM(x)));
        }
    }
}