using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Mithril.Core.Extensions
{
    /// <summary>
    /// IEndpointRouteBuilder extensions
    /// </summary>
    public static class IEndpointRouteBuilderExtensions
    {
        /// <summary>
        /// Determines whether this instance is setup.
        /// </summary>
        /// <param name="endpointRouteBuilder">The endpoint route builder.</param>
        /// <returns>
        /// <c>true</c> if the specified endpoint route builder is setup; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsSetup(this IEndpointRouteBuilder? endpointRouteBuilder) => endpointRouteBuilder?.ServiceProvider.GetService<MithrilSetup>() is not null;
    }

    /// <summary>
    /// Checks if Mithril is setup
    /// </summary>
    internal class MithrilSetup
    {
    }
}