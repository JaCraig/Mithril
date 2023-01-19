namespace Mithril.Apm.Abstractions.Interfaces
{
    /// <summary>
    /// Metrics reporter interface
    /// </summary>
    public interface IMetricsReporter
    {
        /// <summary>
        /// Batches the specified data for reporting.
        /// </summary>
        /// <param name="data">The data.</param>
        void Batch(Dictionary<string, TraceInformation> data);
    }
}