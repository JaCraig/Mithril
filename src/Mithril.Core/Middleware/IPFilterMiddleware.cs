using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Mithril.Core.Abstractions.Services;
using System.Net;

namespace Mithril.Core.Middleware
{
    /// <summary>
    /// IP Filter Middleware
    /// </summary>
    public class IPFilterMiddleware
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IPFilterMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        /// <param name="iPFilterService">The i p filter service.</param>
        /// <param name="logger">The logger.</param>
        public IPFilterMiddleware(RequestDelegate? next, IIPFilterService? iPFilterService, ILogger<IPFilterMiddleware>? logger)
        {
            _next = next;
            IPFilterService = iPFilterService;
            Logger = logger;
        }

        /// <summary>
        /// The next
        /// </summary>
        private readonly RequestDelegate? _next;

        /// <summary>
        /// The ip filter service
        /// </summary>
        private readonly IIPFilterService? IPFilterService;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<IPFilterMiddleware>? Logger;

        /// <summary>
        /// Invokes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Async task</returns>
        public Task InvokeAsync(HttpContext context)
        {
            if (context is null)
                return Task.CompletedTask;
            if (!(IPFilterService?.CheckIPAllowed(context, "DefaultPolicy") ?? true))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                Logger?.LogWarning("Request from remote IP address blocked: {RemoteIP}", context.Connection.RemoteIpAddress);
                return Task.CompletedTask;
            }
            return _next?.Invoke(context) ?? Task.CompletedTask;
        }
    }
}