using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Mithril.API.Abstractions.Query;
using Mithril.API.Abstractions.Query.BaseClasses;
using Mithril.API.Abstractions.Query.Interfaces;
using Mithril.API.Abstractions.Query.ViewModels;
using Mithril.API.Abstractions.Services;
using System.Security.Claims;

namespace Mithril.API.GraphQL.Queries
{
    /// <summary>
    /// Simple entry point for drop down style key/value pair queries.
    /// TODO: Add Tests
    /// </summary>
    /// <seealso cref="QueryBaseClass&lt;IEnumerable&lt;DropDownVM&lt;long&gt;&gt;&gt;"/>
    public class DropDownQuery : QueryBaseClass<IEnumerable<DropDownVM<long>>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DropDownQuery"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="featureManager">The feature manager.</param>
        /// <param name="dropDownQueryService">The drop down query service.</param>
        public DropDownQuery(ILogger<DropDownQuery>? logger, IFeatureManager? featureManager, IDropDownQueryService dropDownQueryService)
            : base(logger, featureManager)
        {
            DropDownQueryService = dropDownQueryService;
        }

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        /// <value>The arguments.</value>
        public override IArgument[] Arguments { get; } = new IArgument[]
        {
            new Argument<string>{ DefaultValue = "", Description = "The data type to query.", Name = "type"},
            new Argument<string>{ DefaultValue = "", Description = "The filter to apply to the query.", Name = "filter"}
        };

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public override string Name => "DropDown";

        /// <summary>
        /// Gets the drop down query service.
        /// </summary>
        /// <value>The drop down query service.</value>
        private IDropDownQueryService DropDownQueryService { get; }

        /// <summary>
        /// Used to resolve the data asked for by the query.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The data specified.</returns>
        public override async Task<IEnumerable<DropDownVM<long>>?> ResolveAsync(ClaimsPrincipal? user, Arguments arguments)
        {
            var QueryType = arguments.GetValue<string>("type");
            var QueryFilter = arguments.GetValue<string>("filter");
            IDropDownQuery? DropDownQuery = DropDownQueryService.FindDropDownQuery(QueryType, user);
            return DropDownQuery is null
                ? new List<DropDownVM<long>>()
                : await DropDownQuery.GetDataAsync(QueryFilter).ConfigureAwait(false);
        }
    }
}