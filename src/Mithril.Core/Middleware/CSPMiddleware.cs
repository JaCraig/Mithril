using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Mithril.Core.Abstractions.Configuration;

namespace Mithril.Core.Middleware
{
    /// <summary>
    /// CSP Middleware
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="CSPMiddleware"/> class.
    /// </remarks>
    /// <param name="next">The next.</param>
    /// <param name="configuration">The configuration.</param>
    public class CSPMiddleware(RequestDelegate? next, IOptions<MithrilConfig>? configuration)
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>The configuration.</value>
        private string Policy { get; } = $"{configuration?.Value?.Security?.ContentSecurityPolicy ?? "default-src 'self'"}; report-uri /api/Command/CSPLog";

        /// <summary>
        /// The next
        /// </summary>
        private readonly RequestDelegate? _next = next;

        /// <summary>
        /// Invokes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Async task</returns>
        public Task InvokeAsync(HttpContext context)
        {
            if (context is null)
                return Task.CompletedTask;
            context.Response.Headers.Append("Content-Security-Policy", Policy);
            return _next?.Invoke(context) ?? Task.CompletedTask;
        }
    }
}