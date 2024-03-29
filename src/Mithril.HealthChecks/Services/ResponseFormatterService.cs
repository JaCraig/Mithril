﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Mithril.HealthChecks.Abstractions.Interfaces;
using Mithril.HealthChecks.Abstractions.Services;

namespace Mithril.HealthChecks.Services
{
    /// <summary>
    /// Response formatter service
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ResponseFormatterService"/> class.
    /// </remarks>
    /// <param name="formatters">The formatters.</param>
    public class ResponseFormatterService(IEnumerable<IResponseFormatter> formatters) : IResponseFormatterService
    {
        /// <summary>
        /// Gets the formatters.
        /// </summary>
        /// <value>The formatters.</value>
        public IEnumerable<IResponseFormatter> Formatters { get; } = formatters.OrderBy(x => x.Order);

        /// <summary>
        /// Formats the response.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <param name="healthReport">The health report.</param>
        /// <returns></returns>
        public Task FormatResponse(HttpContext httpContext, HealthReport healthReport)
        {
            if (httpContext is null)
                return Task.CompletedTask;
            httpContext.Response.StatusCode = healthReport.Status == HealthStatus.Healthy ? 200 : 500;
            IResponseFormatter? Formatter = Formatters.FirstOrDefault(x => x.Accepts(httpContext.Request.RouteValues["format"]?.ToString() ?? ""));
            return Formatter is null ? Task.CompletedTask : Formatter.SendResponseAsync(httpContext, healthReport);
        }
    }
}