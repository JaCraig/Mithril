using Mithril.Routing.Abstractions.Interfaces;

namespace Mithril.Routing.Abstractions.Services
{
    /// <summary>
    /// Route manager
    /// </summary>
    public interface IRouteService
    {
        /// <summary>
        /// Adds the route.
        /// </summary>
        /// <param name="inputPath">The input path.</param>
        /// <param name="outputPath">The output path.</param>
        /// <returns>This.</returns>
        Task<IRouteService> AddRouteAsync(string inputPath, string outputPath);

        /// <summary>
        /// Gets the route.
        /// </summary>
        /// <param name="inputPath">The input path.</param>
        /// <returns>The route specified.</returns>
        IRoute? GetRoute(string? inputPath);
    }
}