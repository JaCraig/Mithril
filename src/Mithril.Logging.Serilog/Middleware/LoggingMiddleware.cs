using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace Mithril.Logging.Serilog.Middleware
{
    /// <summary>
    /// Logging middleware used to store info for Serilog.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="LoggingMiddleware"/> class.
    /// </remarks>
    /// <param name="next">The next.</param>
    public class LoggingMiddleware(RequestDelegate? next)
    {
        /// <summary>
        /// The next
        /// </summary>
        private readonly RequestDelegate? _next = next;

        /// <summary>
        /// Invokes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            if (context is null || _next is null)
                return;
            using (LogContext.PushProperty("UserName", context.User?.Identity?.Name ?? ""))
            {
                await _next.Invoke(context).ConfigureAwait(false);
            }
        }
    }
}