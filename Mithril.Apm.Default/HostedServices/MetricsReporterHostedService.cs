using BigBook;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mithril.Apm.Abstractions.Configuration;
using Mithril.Apm.Abstractions.Services;
using Mithril.Apm.Default.Models;
using Mithril.Data.Abstractions.Services;

namespace Mithril.Apm.Default.HostedServices
{
    /// <summary>
    /// Metrics reporter hosted service
    /// </summary>
    /// <seealso cref="IHostedService"/>
    /// <seealso cref="IDisposable"/>
    public class MetricsReporterHostedService : IHostedService, IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MetricsReporterHostedService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="metricsCollectorService">The metrics collector service.</param>
        /// <param name="dataService">The data service.</param>
        /// <param name="options">The options.</param>
        public MetricsReporterHostedService(ILogger<MetricsReporterHostedService>? logger, IMetricsCollectorService? metricsCollectorService, IDataService? dataService, IOptions<APMOptions>? options)
        {
            Logger = logger;
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
        /// Gets or sets the internal timer.
        /// </summary>
        /// <value>The internal timer.</value>
        private Timer? InternalTimer { get; set; }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        private ILogger<MetricsReporterHostedService>? Logger { get; }

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
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Triggered when the application host is ready to start the service.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the start process has been aborted.</param>
        /// <returns>The async task.</returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Logger?.LogInformation("Starting metrics reporter background service");
            InternalTimer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(Options?.BatchingFrequency ?? 10));
            return Task.CompletedTask;
        }

        /// <summary>
        /// Triggered when the application host is performing a graceful shutdown.
        /// </summary>
        /// <param name="cancellationToken">
        /// Indicates that the shutdown process should no longer be graceful.
        /// </param>
        /// <returns>The async task.</returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            Logger?.LogInformation("Stopping metrics reporter background service");
            InternalTimer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="managed">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release
        /// only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool managed)
        {
            if (managed)
            {
                InternalTimer?.Dispose();
                InternalTimer = null;
            }
        }

        /// <summary>
        /// Does the work.
        /// </summary>
        /// <param name="state">The state.</param>
        private void DoWork(object? state)
        {
            if (MetricsCollectorService is null)
                return;
            Logger?.LogInformation("Reporting APM metrics");
            MetricsCollectorService.BatchCollectedMetrics();

            if (DataService is null)
                return;
            Logger?.LogInformation("Cleaning APM metrics");
            var MaxAge = DateTime.UtcNow.AddHours(-(Options?.MaximumAge ?? 1));
            AsyncHelper.RunSync(() => DataService.DeleteAsync(null, RequestTrace.Query(DataService)?.Where(x => x.DateCreated <= MaxAge).ToList().ToArray() ?? Array.Empty<RequestTrace>()));
        }
    }
}