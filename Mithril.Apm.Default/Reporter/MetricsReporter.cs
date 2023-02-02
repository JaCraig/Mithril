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
    public class MetricsReporter : IMetricsReporter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MetricsReporter"/> class.
        /// </summary>
        /// <param name="dataService">The data service.</param>
        public MetricsReporter(IDataService? dataService)
        {
            DataService = dataService;
        }

        /// <summary>
        /// Gets the data service.
        /// </summary>
        /// <value>The data service.</value>
        private IDataService? DataService { get; }

        /// <summary>
        /// Batches the specified data for reporting.
        /// </summary>
        /// <param name="data">The data.</param>
        public void Batch(Dictionary<string, TraceInformation> data)
        {
            if (DataService is null)
                return;
            var Requests = new List<RequestTrace>();
            foreach (var entry in data)
            {
                if (entry.Value.MetaData.Count == 0 && entry.Value.Metrics.Count == 0)
                    continue;
                var Trace = RequestTrace.Query(DataService)?.Where(x => x.TraceIdentifier == entry.Key && x.DateCreated == entry.Value.Created).FirstOrDefault() ?? new RequestTrace(entry.Key)
                {
                    DateCreated = entry.Value.Created
                };
                foreach (var MetaData in entry.Value.MetaData)
                {
                    foreach (var Entry in MetaData.Data)
                    {
                        Trace.AddMetaData(Entry.Key, Entry.Value);
                    }
                }
                foreach (var Metric in entry.Value.Metrics)
                {
                    foreach (var Entry in Metric.Data)
                    {
                        Trace.AddMetrics(Entry.Key, Metric.MetaData, Entry.Value);
                    }
                }
                Requests.Add(Trace);
                if (Requests.Count >= 40)
                {
                    AsyncHelper.RunSync(() => DataService.SaveAsync(Requests.ToArray()));
                    Requests.Clear();
                }
            }

            AsyncHelper.RunSync(() => DataService.SaveAsync(Requests.ToArray()));
        }
    }
}