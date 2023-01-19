using Mithril.Apm.Abstractions.Interfaces;

namespace Mithril.Apm.Abstractions
{
    /// <summary>
    /// Metrics Entry
    /// </summary>
    /// <param name="Source">Source it came from.</param>
    /// <param name="Data">Data</param>
    /// <param name="TraceIdentifier">Trace identifier</param>
    /// <seealso cref="IEquatable&lt;MetricsEntry&gt;"/>
    public record MetricsEntry(IMetricsCollector Source, string TraceIdentifier, KeyValuePair<string, double> Data);
}