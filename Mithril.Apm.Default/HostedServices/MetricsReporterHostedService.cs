using BigBook;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mithril.Apm.Abstractions.Configuration;
using Mithril.Apm.Abstractions.Services;
using Mithril.Apm.Default.Models;
using Mithril.Core.Abstractions.BaseClasses;
using Mithril.Data.Abstractions.Services;

namespace Mithril.Apm.Default.HostedServices
{
    /// <summary>
    /// Metrics reporter hosted service
    /// </summary>
    /// <seealso cref="IHostedService"/>
    /// <seealso cref="IDisposable"/>
    public class MetricsReporterHostedService : HostedServiceBaseClass<MetricsReporterHostedService>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MetricsReporterHostedService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="metricsCollectorService">The metrics collector service.</param>
        /// <param name="dataService">The data service.</param>
        /// <param name="options">The options.</param>
        public MetricsReporterHostedService(ILogger<MetricsReporterHostedService>? logger, IMetricsCollectorService? metricsCollectorService, IDataService? dataService, IOptions<APMOptions>? options)
            : base(logger, options?.Value?.BatchingFrequency ?? 10)
        {
            MetricsCollectorService = metricsCollectorService;
            DataService = dataService;
            Options = options?.Value;
        }

        /// <summary>
        /// Gets the data service.
        /// </summary>
        /// <value>The data service.</value>
        private IDataService? DataService { get; }

        /// <summary>
        /// Gets the metrics collector service.
        /// </summary>
        /// <value>The metrics collector service.</value>
        private IMetricsCollectorService? MetricsCollectorService { get; }

        /// <summary>
        /// Gets the options.
        /// </summary>
        /// <value>The options.</value>
        private APMOptions? Options { get; }

        /// <summary>
        /// Does the work
        /// </summary>
        /// <returns>Async task.</returns>
        protected override Task DoWorkAsync()
        {
            if (MetricsCollectorService is null)
                return Task.CompletedTask;
            Logger?.LogInformation("Reporting APM metrics");
            MetricsCollectorService.BatchCollectedMetrics();

            if (DataService is null)
                return Task.CompletedTask;
            Logger?.LogInformation("Cleaning APM metrics");
            var MaxAge = DateTime.UtcNow.AddHours(-(Options?.MaximumAge ?? 1));
            return DataService.DeleteAsync(null, RequestTrace.Query(DataService)?.Where(x => x.DateCreated <= MaxAge).ToList().ToArray() ?? Array.Empty<RequestTrace>());
        }
    }
}