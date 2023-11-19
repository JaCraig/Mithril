using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mithril.Apm.Abstractions.Configuration;
using Mithril.Apm.Abstractions.Services;
using Mithril.Apm.Default.Models;
using Mithril.Background.Abstractions.Frequencies;
using Mithril.Background.Abstractions.Interfaces;
using Mithril.Data.Abstractions.Services;

namespace Mithril.Apm.Default.BackgroundTasks
{
    /// <summary>
    /// Metrics reporter background task
    /// </summary>
    /// <seealso cref="IScheduledTask" />
    /// <remarks>
    /// Initializes a new instance of the <see cref="MetricsReporterBackgroundTask"/> class.
    /// </remarks>
    /// <param name="logger">The logger.</param>
    /// <param name="metricsCollectorService">The metrics collector service.</param>
    /// <param name="dataService">The data service.</param>
    /// <param name="options">The options.</param>
    public class MetricsReporterBackgroundTask(ILogger<MetricsReporterBackgroundTask>? logger, IMetricsCollectorService? metricsCollectorService, IDataService? dataService, IOptions<APMOptions>? options) : IScheduledTask
    {
        /// <summary>
        /// The lock object
        /// </summary>
        private readonly SemaphoreSlim LockObject = new(1, 1);

        /// <summary>
        /// Gets the frequencies that the task is run at.
        /// </summary>
        /// <value>
        /// The frequencies the task is run at.
        /// </value>
        public IFrequency[] Frequencies { get; } = [new RunEvery(TimeSpan.FromSeconds(options?.Value?.BatchingFrequency ?? 10))];

        /// <summary>
        /// Gets the last run time.
        /// </summary>
        /// <value>
        /// The last run time.
        /// </value>
        public DateTime LastRunTime { get; set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; } = "APM Metrics Reporting";

        /// <summary>
        /// Gets the data service.
        /// </summary>
        /// <value>The data service.</value>
        private IDataService? DataService { get; } = dataService;

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        private ILogger<MetricsReporterBackgroundTask>? Logger { get; } = logger;

        /// <summary>
        /// Gets the metrics collector service.
        /// </summary>
        /// <value>The metrics collector service.</value>
        private IMetricsCollectorService? MetricsCollectorService { get; } = metricsCollectorService;

        /// <summary>
        /// Gets the options.
        /// </summary>
        /// <value>The options.</value>
        private APMOptions? Options { get; } = options?.Value;

        /// <summary>
        /// Executes this instance.
        /// </summary>
        public async Task ExecuteAsync()
        {
            if (MetricsCollectorService is null)
                return;
            Logger?.LogInformation("Reporting APM metrics");
            _ = MetricsCollectorService.BatchCollectedMetrics();

            if (DataService is null)
                return;
            await LockObject.WaitAsync();
            try
            {
                Logger?.LogInformation("Cleaning APM metrics");
                DateTime MaxAge = DateTime.UtcNow.AddHours(-(Options?.MaximumAge ?? 1));
                RequestTrace[] OldTraces = RequestTrace.Query(DataService)?.Where(x => x.DateCreated <= MaxAge).ToList().ToArray() ?? [];
                _ = await DataService.DeleteAsync(null, OldTraces).ConfigureAwait(false);
            }
            finally { _ = LockObject.Release(); }
        }
    }
}