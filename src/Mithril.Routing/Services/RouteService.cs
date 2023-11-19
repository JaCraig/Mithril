using Mithril.Data.Abstractions.Services;
using Mithril.Routing.Abstractions.Interfaces;
using Mithril.Routing.Abstractions.Services;
using Mithril.Routing.Models;

namespace Mithril.Routing.Services
{
    /// <summary>
    /// Route service
    /// </summary>
    /// <seealso cref="IRouteService"/>
    /// <remarks>
    /// Initializes a new instance of the <see cref="RouteService"/> class.
    /// </remarks>
    /// <param name="dataService">The data service.</param>
    public class RouteService(IDataService? dataService) : IRouteService
    {
        /// <summary>
        /// Gets the data service.
        /// </summary>
        /// <value>The data service.</value>
        private IDataService? DataService { get; } = dataService;

        /// <summary>
        /// Adds the route.
        /// </summary>
        /// <param name="inputPath">The input path.</param>
        /// <param name="outputPath">The output path.</param>
        /// <returns>This.</returns>
        public async Task<IRouteService> AddRouteAsync(string inputPath, string outputPath)
        {
            _ = await RouteEntry.LoadOrCreateAsync(inputPath, outputPath, DataService, null).ConfigureAwait(false);
            return this;
        }

        /// <summary>
        /// Gets the route.
        /// </summary>
        /// <param name="inputPath">The input path.</param>
        /// <returns>The route specified.</returns>
        public IRoute? GetRoute(string? inputPath) => RouteEntry.Load(inputPath, DataService);
    }
}