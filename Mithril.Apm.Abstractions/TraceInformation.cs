namespace Mithril.Apm.Abstractions
{
    /// <summary>
    /// Trace information
    /// </summary>
    public class TraceInformation
    {
        /// <summary>
        /// Gets or sets the when ran.
        /// </summary>
        /// <value>The when ran.</value>
        public DateTime Created { get; } = DateTime.UtcNow;

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>The data.</value>
        public List<MetaDataEntry> MetaData { get; } = new List<MetaDataEntry>();

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