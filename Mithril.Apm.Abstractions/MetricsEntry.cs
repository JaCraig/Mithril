using Mithril.Apm.Abstractions.Interfaces;

namespace Mithril.Apm.Abstractions
{
    /// <summary>
    /// Metrics Entry
    /// </summary>
    public readonly record struct MetricsEntry(IMetricsCollector Source, string TraceIdentifier, string MetaData, KeyValuePair<string, decimal>[] Data);
}