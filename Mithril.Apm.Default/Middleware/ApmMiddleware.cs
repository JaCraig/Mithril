using Microsoft.AspNetCore.Http;
using Mithril.Apm.Abstractions.Interfaces;
using Mithril.Apm.Abstractions.Services;
using Mithril.Apm.Default.Sources;
using Mithril.Apm.Default.TraceData;
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
            Source = metricsCollectorService?.GetMetricsCollector(nameof(RequestSource));
            TraceDataCollector = metricsCollectorService?.GetTraceDataCollector(nameof(DefaultTraceData));
        }

        /// <summary>
        /// Gets the metrics collector service.
        /// </summary>
        /// <value>The metrics collector service.</value>
        private IMetricsCollector? Source { get; }

        /// <summary>
        /// Gets the trace data collector.
        /// </summary>
        /// <value>The trace data collector.</value>
        private ITraceDataCollector? TraceDataCollector { get; }

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
            var StartTimeTicks = Stopwatch.GetTimestamp();

            await next.Invoke(context).ConfigureAwait(false);

            var StopTimeTicks = Stopwatch.GetTimestamp();

            Source?.AddEntry(context.TraceIdentifier, new KeyValuePair<string, double>("Total Transaction Time", (StopTimeTicks - StartTimeTicks) / 1000));
            TraceDataCollector?.AddEntry(context.TraceIdentifier, new KeyValuePair<string, string>("Path", context.Request.Path));
            TraceDataCollector?.AddEntry(context.TraceIdentifier, new KeyValuePair<string, string>("Method", context.Request.Method));
        }
    }
}