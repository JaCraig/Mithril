using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Mithril.Core.Abstractions.Configuration;

namespace Mithril.Core.Middleware
{
    /// <summary>
    /// X-Frame-Options Middleware
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="XFrameOptionsMiddleware"/> class.
    /// </remarks>
    /// <param name="next">The next.</param>
    /// <param name="configuration">The configuration.</param>
    public class XFrameOptionsMiddleware(RequestDelegate? next, IOptions<MithrilConfig>? configuration)
    {
        /// <summary>
        /// Gets or sets the options.
        /// </summary>
        /// <value>The options.</value>
        private string? Options { get; } = configuration?.Value?.Security?.XFrameOptions ?? "deny";

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
            context.Response.Headers.Append("X-Frame-Options", Options);
            return _next?.Invoke(context) ?? Task.CompletedTask;
        }
    }
}