﻿using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Mithril.API.Abstractions.Query;
using Mithril.API.Abstractions.Query.BaseClasses;
using Mithril.API.Abstractions.Query.Interfaces;
using Mithril.API.Abstractions.Query.ViewModels;
using Mithril.API.Abstractions.Services;
using Mithril.Data.Abstractions.Services;
using System.Security.Claims;

namespace Mithril.API.GraphQL.Queries
{
    /// <summary>
    /// Simple entry point for drop down style key/value pair queries.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="DropDownQuery" /> class.
    /// </remarks>
    /// <param name="logger">The logger.</param>
    /// <param name="featureManager">The feature manager.</param>
    /// <param name="dropDownQueryService">The drop down query service.</param>
    /// <param name="dataService">The data service.</param>
    public class DropDownQuery(ILogger<DropDownQuery>? logger, IFeatureManager? featureManager, IDropDownQueryService? dropDownQueryService, IDataService? dataService) : QueryBaseClass<IEnumerable<DropDownVM<long>>>(logger, featureManager)
    {
        /// <summary>
        /// Gets the arguments.
        /// </summary>
        /// <value>The arguments.</value>
        public override IArgument[] Arguments { get; } =
        [
            new Argument<string> { DefaultValue = "", Description = "The data type to query.", Name = "type" },
            new Argument<string> { DefaultValue = "", Description = "The filter to apply to the query.", Name = "filter" }
        ];

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public override string Name => "DropDown";

        /// <summary>
        /// Gets the data service.
        /// </summary>
        /// <value>
        /// The data service.
        /// </value>
        private IDataService? DataService { get; } = dataService;

        /// <summary>
        /// Gets the drop down query service.
        /// </summary>
        /// <value>The drop down query service.</value>
        private IDropDownQueryService? DropDownQueryService { get; } = dropDownQueryService;

        /// <summary>
        /// Used to resolve the data asked for by the query.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The data specified.</returns>
        public override async Task<IEnumerable<DropDownVM<long>>?> ResolveAsync(ClaimsPrincipal? user, Arguments arguments)
        {
            var QueryType = arguments?.GetValue<string>("type") ?? "";
            var QueryFilter = arguments?.GetValue<string>("filter") ?? "";
            IDropDownQuery? DropDownQuery = DropDownQueryService?.FindDropDownQuery(QueryType, user);
            return DropDownQuery is null
                ? new List<DropDownVM<long>>()
                : await DropDownQuery.GetDataAsync(DataService, QueryFilter).ConfigureAwait(false);
        }
    }
}