using Mithril.Apm.Abstractions.Interfaces;

namespace Mithril.Apm.Abstractions
{
    /// <summary>
    /// Trace entry
    /// </summary>
    /// <param name="Source">Source this came from</param>
    /// <param name="TraceIdentifier">The trace identifier.</param>
    /// <param name="Data">Data to save.</param>
    /// <seealso cref="IEquatable&lt;TraceEntry&gt;"/>
    public readonly record struct MetaDataEntry(IMetaDataCollector Source, string TraceIdentifier, KeyValuePair<string, string>[] Data);
}