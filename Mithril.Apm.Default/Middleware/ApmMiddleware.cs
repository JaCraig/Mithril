using Microsoft.AspNetCore.Http;
using Mithril.Apm.Abstractions;
using Mithril.Apm.Abstractions.Interfaces;
using Mithril.Apm.Abstractions.Services;
using System.Diagnostics;

namespace Mithril.Apm.Default.Middleware
{
    /// <summary>
    /// APM collection middleware
    /// </summary>
    /// <seealso cref="IMiddleware"/>
    public class ApmMiddleware : IMiddleware
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApmMiddleware"/> class.
        /// </summary>
        /// <param name="metricsCollectorService">The metrics collector service.</param>
        public ApmMiddleware(IMetricsCollectorService? metricsCollectorService)
        {
            MetricsCollector = metricsCollectorService?.GetMetricsCollector(nameof(DefaultMetricsCollector));
            MetaDataCollector = metricsCollectorService?.GetMetaDataCollector(nameof(DefaultMetaDataCollector));
        }

        /// <summary>
        /// Gets the trace data collector.
        /// </summary>
        /// <value>The trace data collector.</value>
        private IMetaDataCollector? MetaDataCollector { get; }

        /// <summary>
        /// Gets the metrics collector service.
        /// </summary>
        /// <value>The metrics collector service.</value>
        private IMetricsCollector? MetricsCollector { get; }

        /// <summary>
        /// Request handling method.
        /// </summary>
        /// <param name="context">
        /// The <see cref="T:Microsoft.AspNetCore.Http.HttpContext"/> for the current request.
        /// </param>
        /// <param name="next">
        /// The delegate representing the remaining middleware in the request pipeline.
        /// </param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task"/> that represents the execution of this middleware.
        /// </returns>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (next is null || context is null)
                return;
            var StartTimeTicks = Stopwatch.GetTimestamp();

            await next.Invoke(context).ConfigureAwait(false);

            var StopTimeTicks = Stopwatch.GetTimestamp();

            MetricsCollector?.AddEntry(context.TraceIdentifier, "Request", new KeyValuePair<string, decimal>("Total Transaction Time", (StopTimeTicks - StartTimeTicks) / 10000));
            MetaDataCollector?.AddEntry(context.TraceIdentifier, new KeyValuePair<string, string>("Path", context.Request.Path), new KeyValuePair<string, string>("Method", context.Request.Method));
        }
    }
}