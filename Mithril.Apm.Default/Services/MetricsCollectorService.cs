using BigBook;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Mithril.Apm.Abstractions;
using Mithril.Apm.Abstractions.Features;
using Mithril.Apm.Abstractions.Interfaces;
using Mithril.Apm.Abstractions.Services;
using Mithril.Core.Abstractions.Extensions;

namespace Mithril.Apm.Default.Services
{
    /// <summary>
    /// Metrics collector service
    /// </summary>
    /// <seealso cref="IMetricsCollectorService"/>
    public class MetricsCollectorService : IMetricsCollectorService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MetricsCollectorService"/> class.
        /// </summary>
        /// <param name="sources">The sources.</param>
        /// <param name="metricsReporters">The metrics reporters.</param>
        /// <param name="traceDataCollectors">The trace data collectors.</param>
        /// <param name="featureManager">The feature manager.</param>
        /// <param name="logger">The logger.</param>
        public MetricsCollectorService(
            IEnumerable<IMetricsCollector> sources,
            IEnumerable<IMetricsReporter> metricsReporters,
            IEnumerable<IMetaDataCollector> traceDataCollectors,
            IFeatureManager featureManager,
            ILogger<MetricsCollectorService> logger)
        {
            Sources = sources ?? Array.Empty<IMetricsCollector>();
            MetricsReporters = metricsReporters ?? Array.Empty<IMetricsReporter>();
            TraceDataCollectors = traceDataCollectors ?? Array.Empty<IMetaDataCollector>();
            FeatureManager = featureManager;
            Logger = logger;
            foreach (var Source in Sources)
            {
                Source.Subscribe(this);
            }
            foreach (var TraceSource in TraceDataCollectors)
            {
                TraceSource.Subscribe(this);
            }
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="MetricsCollectorService"/> class.
        /// </summary>
        ~MetricsCollectorService()
        {
            Dispose(disposing: false);
        }

        /// <summary>
        /// Gets the feature manager.
        /// </summary>
        /// <value>The feature manager.</value>
        private IFeatureManager FeatureManager { get; }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        private ILogger<MetricsCollectorService> Logger { get; }

        /// <summary>
        /// Gets the metrics reporters.
        /// </summary>
        /// <value>The metrics reporters.</value>
        private IEnumerable<IMetricsReporter> MetricsReporters { get; set; }

        /// <summary>
        /// Gets the sources.
        /// </summary>
        /// <value>The sources.</value>
        private IEnumerable<IMetricsCollector> Sources { get; set; }

        /// <summary>
        /// Gets the trace data collectors.
        /// </summary>
        /// <value>The trace data collectors.</value>
        private IEnumerable<IMetaDataCollector> TraceDataCollectors { get; }

        /// <summary>
        /// Gets or sets the trace information.
        /// </summary>
        /// <value>The trace information.</value>
        private Dictionary<string, TraceInformation> TraceInformation { get; } = new Dictionary<string, TraceInformation>();

        /// <summary>
        /// The lock object
        /// </summary>
        private static readonly object LockObject = new();

        /// <summary>
        /// The disposed value
        /// </summary>
        private bool disposedValue;

        /// <summary>
        /// Reports the collected metrics to the registered reporters.
        /// </summary>
        /// <returns>This.</returns>
        public IMetricsCollectorService BatchCollectedMetrics()
        {
            if (!FeatureManager.AreFeaturesEnabled(APMFeature.Instance))
            {
                lock (LockObject)
                {
                    TraceInformation.Clear();
                }
                return this;
            }
            var TempData = new Dictionary<string, TraceInformation>();
            lock (LockObject)
            {
                TraceInformation.CopyTo(TempData);
                TraceInformation.Clear();
            }
            foreach (var Reporter in MetricsReporters)
            {
                Reporter.Batch(TempData);
            }
            return this;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Gets the source specified.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The metric source object specified.</returns>
        public IMetricsCollector? GetMetricsCollector(string name)
        {
            if (!FeatureManager.AreFeaturesEnabled(APMFeature.Instance))
                return null;
            return Sources.FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Gets the trace data collector.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The trace data collector specified.</returns>
        public IMetaDataCollector? GetTraceDataCollector(string name)
        {
            if (!FeatureManager.AreFeaturesEnabled(APMFeature.Instance))
                return null;
            return TraceDataCollectors.FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Notifies the observer that the provider has finished sending push-based notifications.
        /// </summary>
        public void OnCompleted()
        {
        }

        /// <summary>
        /// Notifies the observer that the provider has experienced an error condition.
        /// </summary>
        /// <param name="error">An object that provides additional information about the error.</param>
        public void OnError(Exception error)
        {
            if (error is null)
                return;
            Logger?.LogError(error, "Issue with gathering APM data.");
        }

        /// <summary>
        /// Provides the observer with new data.
        /// </summary>
        /// <param name="value">The current notification information.</param>
        public void OnNext(MetricsEntry value)
        {
            GetTraceInformation(value.TraceIdentifier).Metrics.Add(value);
        }

        /// <summary>
        /// Provides the observer with new data.
        /// </summary>
        /// <param name="value">The current notification information.</param>
        public void OnNext(MetaDataEntry value)
        {
            GetTraceInformation(value.TraceIdentifier).MetaData.Add(value);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release
        /// only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    foreach (var Source in Sources)
                    {
                        Source.Dispose();
                    }
                    Sources = new List<IMetricsCollector>();
                    MetricsReporters = new List<IMetricsReporter>();
                }
                disposedValue = true;
            }
        }

        /// <summary>
        /// Gets the trace information.
        /// </summary>
        /// <param name="traceIdentifier">The trace identifier.</param>
        /// <returns>The trace information.</returns>
        private TraceInformation GetTraceInformation(string traceIdentifier)
        {
            if (TraceInformation.TryGetValue(traceIdentifier, out var Trace))
                return Trace;
            lock (LockObject)
            {
                if (TraceInformation.TryGetValue(traceIdentifier, out Trace))
                    return Trace;
                Trace = new TraceInformation
                {
                    TraceIdentifier = traceIdentifier
                };
                TraceInformation.Add(traceIdentifier, Trace);
            }
            return Trace;
        }
    }
}