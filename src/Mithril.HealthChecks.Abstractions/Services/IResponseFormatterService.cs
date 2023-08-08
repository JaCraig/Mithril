using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Mithril.HealthChecks.Abstractions.Services
{
    /// <summary>
    /// Response formatter service interface
    /// </summary>
    public interface IResponseFormatterService
    {
        /// <summary>
        /// Formats the response.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <param name="healthReport">The health report.</param>
        /// <returns>The async task</returns>
        Task FormatResponse(HttpContext httpContext, HealthReport healthReport);
    }
}