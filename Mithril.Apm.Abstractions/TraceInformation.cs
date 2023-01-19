namespace Mithril.Apm.Abstractions
{
    /// <summary>
    /// Trace information
    /// </summary>
    public class TraceInformation
    {
        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>The data.</value>
        public Dictionary<string, string> Data { get; } = new Dictionary<string, string>();

        /// <summary>
        /// Gets the metrics.
        /// </summary>
        /// <value>The metrics.</value>
        public List<MetricsEntry> Metrics { get; } = new List<MetricsEntry>();

        /// <summary>
        /// Gets or sets the trace identifier.
        /// </summary>
        /// <value>The trace identifier.</value>
        public string? TraceIdentifier { get; set; }
    }
}