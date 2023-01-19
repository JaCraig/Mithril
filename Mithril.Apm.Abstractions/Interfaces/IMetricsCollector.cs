namespace Mithril.Apm.Abstractions.Interfaces
{
    /// <summary>
    /// Metrics source interface
    /// </summary>
    public interface IMetricsCollector : IObservable<MetricsEntry>, IDisposable
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        /// Adds an entry to the collector.
        /// </summary>
        /// <param name="traceId">The trace identifier.</param>
        /// <param name="entry">The entry.</param>
        /// <returns>This.</returns>
        public IMetricsCollector AddEntry(string traceId, KeyValuePair<string, double> entry);
    }
}