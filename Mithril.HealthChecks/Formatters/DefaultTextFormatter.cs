using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Mithril.HealthChecks.Abstractions.BaseClasses;

namespace Mithril.HealthChecks.Formatters
{
    /// <summary>
    /// Default text formatter
    /// </summary>
    /// <seealso cref="ResponseFormatterBaseClass"/>
    public class DefaultTextFormatter : ResponseFormatterBaseClass
    {
        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        public override int Order => int.MaxValue;

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
            httpContext.Response.ContentType = "text/plain";
            return httpContext.Response.WriteAsync(healthReport.Status.ToString());
        }
    }
}