using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Mithril.HealthChecks.Abstractions.BaseClasses;

namespace Mithril.HealthChecks.Formatters
{
    /// <summary>
    /// Default json formatter
    /// </summary>
    /// <seealso cref="ResponseFormatterBaseClass"/>
    public class DefaultJsonFormatter : ResponseFormatterBaseClass
    {
        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        public override int Order => int.MaxValue;

        /// <summary>
        /// Acceptses the specified format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <returns>True if it accepts that format, false otherwise.</returns>
        public override bool Accepts(string format) => string.Equals(format, "json", StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Sends the response asynchronously.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <param name="healthReport">The health report.</param>
        /// <returns>The async task.</returns>
        public override Task SendResponseAsync(HttpContext httpContext, HealthReport healthReport)
        {
            if (httpContext is null)
                return Task.CompletedTask;
            httpContext.Response.ContentType = "text/json";
            return httpContext.Response.WriteAsJsonAsync(healthReport);
        }
    }
}