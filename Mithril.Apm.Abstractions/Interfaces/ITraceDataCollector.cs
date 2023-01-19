namespace Mithril.Apm.Abstractions.Interfaces
{
    /// <summary>
    /// Trace data collector
    /// </summary>
    public interface ITraceDataCollector : IObservable<TraceEntry>, IDisposable
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
        public ITraceDataCollector AddEntry(string traceId, KeyValuePair<string, string> entry);
    }
}