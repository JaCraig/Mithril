using Mithril.Apm.Abstractions;
using Mithril.Apm.Abstractions.Interfaces;

namespace Mithril.Apm.Default.Reporter
{
    public class MetricsReporter : IMetricsReporter
    {
        public void Batch(Dictionary<string, TraceInformation> data)
        {
        }
    }
}