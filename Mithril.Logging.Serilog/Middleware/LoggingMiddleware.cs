using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace Mithril.Logging.Serilog.Middleware
{
    /// <summary>
    /// Logging middleware used to store info for Serilog.
    /// </summary>
    public class LoggingMiddleware
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        public LoggingMiddleware(RequestDelegate? next)
        {
            _next = next;
        }

        /// <summary>
        /// The next
        /// </summary>
        private readonly RequestDelegate? _next;

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