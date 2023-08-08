using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Mithril.HealthChecks.Abstractions.Interfaces
{
    /// <summary>
    /// Response formatter
    /// </summary>
    public interface IResponseFormatter
    {
        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        int Order { get; }

        /// <summary>
        /// Acceptses the specified format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <returns>True if it accepts that format, false otherwise.</returns>
        bool Accepts(string format);

        /// <summary>
        /// Sends the response asynchronously.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <param name="healthReport">The health report.</param>
        /// <returns>The async task.</returns>
        Task SendResponseAsync(HttpContext httpContext, HealthReport healthReport);
    }
}