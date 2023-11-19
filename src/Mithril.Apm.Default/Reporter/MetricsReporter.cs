using BigBook;
using Mithril.Apm.Abstractions;
using Mithril.Apm.Abstractions.Interfaces;
using Mithril.Apm.Default.Models;
using Mithril.Data.Abstractions.Services;

namespace Mithril.Apm.Default.Reporter
{
    /// <summary>
    /// Metrics reporter
    /// </summary>
    /// <seealso cref="IMetricsReporter"/>
    /// <remarks>
    /// Initializes a new instance of the <see cref="MetricsReporter"/> class.
    /// </remarks>
    /// <param name="dataService">The data service.</param>
    public class MetricsReporter(IDataService? dataService) : IMetricsReporter
    {
        /// <summary>
        /// Gets the data service.
        /// </summary>
        /// <value>The data service.</value>
        private IDataService? DataService { get; } = dataService;

        /// <summary>
        /// Batches the specified data for reporting.
        /// </summary>
        /// <param name="data">The data.</param>
        public void Batch(Dictionary<string, TraceInformation> data)
        {
            if (DataService is null)
                return;
            var Requests = new List<RequestTrace>();
            foreach (KeyValuePair<string, TraceInformation> entry in data)
            {
                if (entry.Value.MetaData.Count == 0 && entry.Value.Metrics.Count == 0)
                    continue;
                RequestTrace Trace = RequestTrace.Query(DataService)?.Where(x => x.TraceIdentifier == entry.Key && x.DateCreated == entry.Value.Created).FirstOrDefault() ?? new RequestTrace(entry.Key)
                {
                    DateCreated = entry.Value.Created
                };
                foreach (MetaDataEntry MetaData in entry.Value.MetaData)
                {
                    foreach (KeyValuePair<string, string> Entry in MetaData.Data)
                    {
                        Trace.AddMetaData(Entry.Key, Entry.Value);
                    }
                }
                foreach (MetricsEntry Metric in entry.Value.Metrics)
                {
                    foreach (KeyValuePair<string, decimal> Entry in Metric.Data)
                    {
                        Trace.AddMetrics(Entry.Key, Metric.MetaData, Entry.Value);
                    }
                }
                Requests.Add(Trace);
                if (Requests.Count >= 40)
                {
                    _ = AsyncHelper.RunSync(() => DataService.SaveAsync(null, Requests.ToArray()));
                    Requests.Clear();
                }
            }

            _ = AsyncHelper.RunSync(() => DataService.SaveAsync(null, Requests.ToArray()));
        }
    }
}