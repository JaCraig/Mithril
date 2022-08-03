using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Mithril.Core.Abstractions.Configuration;

namespace Mithril.Core.Middleware
{
    /// <summary>
    /// X-Frame-Options Middleware
    /// </summary>
    public class XFrameOptionsMiddleware
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XFrameOptionsMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        public XFrameOptionsMiddleware(RequestDelegate next, IOptions<MithrilConfig> configuration)
        {
            _next = next;
            Options = configuration?.Value?.Security?.XFrameOptions ?? "deny";
        }

        /// <summary>
        /// Gets or sets the options.
        /// </summary>
        /// <value>The options.</value>
        private string? Options { get; }

        /// <summary>
        /// The next
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// Invokes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Async task</returns>
        public Task InvokeAsync(HttpContext context)
        {
            if (context is null)
                return Task.CompletedTask;
            context.Response.Headers.Add("X-Frame-Options", Options);
            return _next.Invoke(context);
        }
    }
}