using Mithril.Apm.Abstractions.Interfaces;

namespace Mithril.Apm.Abstractions.Services
{
    /// <summary>
    /// Metrics collector service
    /// </summary>
    /// <seealso cref="IObserver&lt;MetricsEntry&gt;"/>
    /// <seealso cref="IDisposable"/>
    public interface IMetricsCollectorService : IObserver<MetricsEntry>, IObserver<MetaDataEntry>, IDisposable
    {
        /// <summary>
        /// Reports the collected metrics to the registered reporters.
        /// </summary>
        /// <returns>This.</returns>
        IMetricsCollectorService BatchCollectedMetrics();

        /// <summary>
        /// Gets the trace data collector.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The trace data collector specified.</returns>
        IMetaDataCollector? GetMetaDataCollector(string name);

        /// <summary>
        /// Gets the source specified.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Metrics source object specified.</returns>
        IMetricsCollector? GetMetricsCollector(string name);
    }
}