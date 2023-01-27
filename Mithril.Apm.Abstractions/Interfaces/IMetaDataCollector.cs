namespace Mithril.Apm.Abstractions.Interfaces
{
    /// <summary>
    /// Trace data collector
    /// </summary>
    public interface IMetaDataCollector : IObservable<MetaDataEntry>, IDisposable
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
        /// <param name="entries">The entries.</param>
        /// <returns>This.</returns>
        public IMetaDataCollector AddEntry(string traceId, params KeyValuePair<string, string>[] entries);
    }
}