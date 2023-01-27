using BigBook;
using Microsoft.Extensions.Logging;
using Mithril.Apm.Abstractions;
using Mithril.Apm.Abstractions.Interfaces;
using Mithril.Apm.Abstractions.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Tracing;

namespace Mithril.Data.Apm
{
    /// <summary>
    /// Query listener
    /// </summary>
    /// <seealso cref="EventListener"/>
    public class QueryListener : EventListener, IMetricsCollector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryListener"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public QueryListener(ILogger<QueryListener> logger)
        {
            Logger = logger;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="MetricsSourceBaseClass"/> class.
        /// </summary>
        ~QueryListener()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; } = nameof(QueryListener);

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        private ILogger<QueryListener> Logger { get; }

        /// <summary>
        /// Gets or sets the metrics collector service.
        /// </summary>
        /// <value>The metrics collector service.</value>
        private IMetricsCollectorService? MetricsCollectorService { get; set; }

        /// <summary>
        /// Gets the observers.
        /// </summary>
        /// <value>The observers.</value>
        private List<IObserver<MetricsEntry>> Observers { get; } = new List<IObserver<MetricsEntry>>();

        /// <summary>
        /// Gets the start time stamps.
        /// </summary>
        /// <value>The start time stamps.</value>
        private Dictionary<int, QueryMetrics> TimeMetrics { get; } = new Dictionary<int, QueryMetrics>();

        /// <summary>
        /// The lock object
        /// </summary>
        private readonly object LockObject = new();

        /// <summary>
        /// The disposed value
        /// </summary>
        private bool disposedValue;

        /// <summary>
        /// Adds an entry to the collector.
        /// </summary>
        /// <param name="traceId">The trace identifier.</param>
        /// <param name="metaData">The meta data.</param>
        /// <param name="entries">The entries.</param>
        /// <returns>This.</returns>
        public IMetricsCollector AddEntry(string traceId, string metaData, params KeyValuePair<string, decimal>[] entries)
        {
            try
            {
                for (var x = 0; x < Observers.Count; ++x)
                {
                    var Observer = Observers[x];
                    Observer.OnNext(new MetricsEntry(this, traceId, metaData, entries));
                }
            }
            catch (Exception ex)
            {
                for (var x = 0; x < Observers.Count; ++x)
                {
                    var Observer = Observers[x];
                    Observer.OnError(ex);
                }
            }
            return this;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Notifies the provider that an observer is to receive notifications.
        /// </summary>
        /// <param name="observer">The object that is to receive notifications.</param>
        /// <returns>
        /// A reference to an interface that allows observers to stop receiving notifications before
        /// the provider has finished sending them.
        /// </returns>
        public IDisposable Subscribe(IObserver<MetricsEntry> observer)
        {
            MetricsCollectorService = observer as IMetricsCollectorService;
            Observers.Add(observer);
            return this;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release
        /// only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }
                disposedValue = true;
            }
        }

        /// <summary>
        /// Called for all existing event sources when the event listener is created and when a new
        /// event source is attached to the listener.
        /// </summary>
        /// <param name="eventSource">The event source.</param>
        protected override void OnEventSourceCreated(EventSource eventSource)
        {
            if (eventSource is null)
            {
                return;
            }
            Logger?.LogInformation("Event source: {Name}", eventSource.Name);
            if (eventSource.Name == "Microsoft.Data.SqlClient.EventSource" || (eventSource.Name == "Microsoft-AdoNet-SystemData" && eventSource.GetType().FullName == "System.Data.SqlEventSource"))
                EnableEvents(eventSource, EventLevel.Informational, (EventKeywords)1);
            base.OnEventSourceCreated(eventSource);
        }

        /// <summary>
        /// Called whenever an event has been written by an event source for which the event
        /// listener has enabled events.
        /// </summary>
        /// <param name="eventData">The event arguments that describe the event.</param>
        protected override void OnEventWritten(EventWrittenEventArgs eventData)
        {
            if (eventData?.Payload is null)
                return;
            try
            {
                switch (eventData.EventId)
                {
                    case 1:
                        BeginProcessing(eventData.Payload);
                        break;

                    case 2:
                        EndProcessing(eventData.Payload);
                        break;
                }
            }
            catch (Exception ex) { Logger.LogError(ex, "Error when attempting to capture SQL query metrics. Event data: {EventData}", eventData); }
        }

        /// <summary>
        /// Begins the processing.
        /// </summary>
        /// <param name="payload">The payload.</param>
        private void BeginProcessing(ReadOnlyCollection<object?> payload)
        {
            var Metrics = GetMetrics(Convert.ToInt32(payload[0]));
            if (Metrics is null)
                return;
            Metrics.DataSource = Convert.ToString(payload[1]);
            Metrics.Database = Convert.ToString(payload[2]);
            Metrics.CommandText = Convert.ToString(payload[3]);
            Metrics.StartTime = Stopwatch.GetTimestamp();
            TimeMetrics.Add(Metrics.ID, Metrics);
        }

        /// <summary>
        /// Ends the processing.
        /// </summary>
        /// <param name="payload">The payload.</param>
        private void EndProcessing(ReadOnlyCollection<object?> payload)
        {
            var Metrics = RemoveMetrics(Convert.ToInt32(payload[0]));
            if (Metrics is null)
                return;
            Metrics.ExceptionNumber = Convert.ToInt32(payload[2]);
            var TraceId = (Metrics.Database + Metrics.DataSource + Metrics.CommandText).Left(100);
            if (Metrics.CommandText?.Contains("RequestTrace_") == true)
                return;
            MetricsCollectorService?.OnNext(new MetaDataEntry(
                null,
                TraceId,
                new[] {
                    new KeyValuePair<string, string>("Database", Metrics.Database??"Default"),
                    new KeyValuePair<string, string>("Datasource", Metrics.DataSource??""),
                    new KeyValuePair<string, string>("CommandText", Metrics.CommandText??""),
                }));
            MetricsCollectorService?.OnNext(new MetricsEntry(
                this,
                TraceId,
                "Database query",
                new[]
                {
                    new KeyValuePair<string, decimal>("Total Query Time",(Stopwatch.GetTimestamp()- Metrics.StartTime)/10000)
                }
                ));
            TimeMetrics.Add(Metrics.ID, Metrics);
        }

        /// <summary>
        /// Gets the metrics.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The metrics object asked for.</returns>
        private QueryMetrics? GetMetrics(int id)
        {
            if (TimeMetrics.TryGetValue(id, out QueryMetrics? metrics))
                return metrics;
            lock (LockObject)
            {
                if (TimeMetrics.TryGetValue(id, out metrics))
                    return metrics;
                metrics = new QueryMetrics()
                {
                    ID = id
                };
                TimeMetrics.Add(id, metrics);
                return metrics;
            }
        }

        /// <summary>
        /// Removes the metrics entry and returns it.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The query metrics object.</returns>
        private QueryMetrics? RemoveMetrics(int id)
        {
            if (TimeMetrics.TryGetValue(id, out QueryMetrics? metrics))
            {
                lock (LockObject)
                {
                    TimeMetrics.Remove(id);
                }
            }
            return metrics;
        }
    }
}