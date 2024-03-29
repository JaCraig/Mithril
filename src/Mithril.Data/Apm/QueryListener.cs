﻿using Microsoft.Extensions.Logging;
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
    /// <seealso cref="IEventListener"/>
    /// <seealso cref="EventListener"/>
    /// <remarks>Initializes a new instance of the <see cref="QueryListener"/> class.</remarks>
    /// <param name="logger">The logger.</param>
    public class QueryListener(ILogger<QueryListener>? logger) : EventListener, IEventListener
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; } = nameof(QueryListener);

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        private ILogger<QueryListener>? Logger { get; } = logger;

        /// <summary>
        /// Gets or sets the meta data collector.
        /// </summary>
        /// <value>The meta data collector.</value>
        private IMetaDataCollector? MetaDataCollector { get; set; }

        /// <summary>
        /// Gets or sets the metrics collector.
        /// </summary>
        /// <value>The metrics collector.</value>
        private IMetricsCollector? MetricsCollector { get; set; }

        /// <summary>
        /// Gets or sets the metrics collector service.
        /// </summary>
        /// <value>The metrics collector service.</value>
        private IMetricsCollectorService? MetricsCollectorService { get; set; }

        /// <summary>
        /// Gets the start time stamps.
        /// </summary>
        /// <value>The start time stamps.</value>
        private Dictionary<int, QueryMetrics> TimeMetrics { get; } = [];

        /// <summary>
        /// The lock object
        /// </summary>
        private readonly object _LockObject = new();

        /// <summary>
        /// The disposed value
        /// </summary>
        private bool _DisposedValue;

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public override void Dispose()
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
        public IEventListener Subscribe(IMetricsCollectorService observer)
        {
            MetricsCollectorService = observer;
            MetaDataCollector = MetricsCollectorService?.GetMetaDataCollector(nameof(DefaultMetaDataCollector));
            MetricsCollector = MetricsCollectorService?.GetMetricsCollector(nameof(DefaultMetricsCollector));
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
            if (!_DisposedValue)
            {
                if (disposing)
                {
                }
                _DisposedValue = true;
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
            if (eventSource.Name == "Microsoft.Data.SqlClient.EventSource")
                EnableEvents(eventSource, EventLevel.Informational, (EventKeywords)1L);
            base.OnEventSourceCreated(eventSource);
        }

        /// <summary>
        /// Called whenever an event has been written by an event source for which the event
        /// listener has enabled events.
        /// </summary>
        /// <param name="eventData">The event arguments that describe the event.</param>
        protected override void OnEventWritten(EventWrittenEventArgs eventData)
        {
            if (eventData?.Payload is null || MetricsCollectorService is null)
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
            catch (Exception Ex) { Logger?.LogError(Ex, "Error when attempting to capture SQL query metrics. Event data: {EventData}", eventData); }
        }

        /// <summary>
        /// Begins the processing.
        /// </summary>
        /// <param name="payload">The payload.</param>
        private void BeginProcessing(ReadOnlyCollection<object?> payload)
        {
            var CommandText = Convert.ToString(payload[3]);
            if (CommandText?.Contains("RequestTrace_") == true)
                return;
            QueryMetrics? Metrics = GetMetrics(Convert.ToInt32(payload[0]));
            if (Metrics is null)
                return;
            Metrics.DataSource = Convert.ToString(payload[1]);
            Metrics.Database = Convert.ToString(payload[2]);
            Metrics.CommandText = Convert.ToString(payload[3]);
            Metrics.StartTime = Stopwatch.GetTimestamp();
        }

        /// <summary>
        /// Ends the processing.
        /// </summary>
        /// <param name="payload">The payload.</param>
        private void EndProcessing(ReadOnlyCollection<object?> payload)
        {
            QueryMetrics? Metrics = RemoveMetrics(Convert.ToInt32(payload[0]));
            if (Metrics is null)
                return;
            Metrics.ExceptionNumber = Convert.ToInt32(payload[2]);
            var TraceId = Guid.NewGuid().ToString();
            if (Metrics.CommandText?.Contains("RequestTrace_") == true)
                return;
            _ = (MetaDataCollector?.AddEntry(TraceId,
                [
                    new KeyValuePair<string, string>("Database", Metrics.Database ?? "Default"),
                    new KeyValuePair<string, string>("Datasource", Metrics.DataSource ?? ""),
                    new KeyValuePair<string, string>("CommandText", Metrics.CommandText ?? ""),
                ]));
            _ = (MetricsCollector?.AddEntry(TraceId, "Database query",
                [
                    new KeyValuePair<string, decimal>("Total Query Time", (Stopwatch.GetTimestamp() - Metrics.StartTime) / 10000L)
                ]));
        }

        /// <summary>
        /// Gets the metrics.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The metrics object asked for.</returns>
        private QueryMetrics? GetMetrics(int id)
        {
            if (TimeMetrics.TryGetValue(id, out QueryMetrics? Metrics))
                return Metrics;
            lock (_LockObject)
            {
                if (TimeMetrics.TryGetValue(id, out Metrics))
                    return Metrics;
                Metrics = new QueryMetrics()
                {
                    ID = id
                };
                TimeMetrics.Add(id, Metrics);
                return Metrics;
            }
        }

        /// <summary>
        /// Removes the metrics entry and returns it.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The query metrics object.</returns>
        private QueryMetrics? RemoveMetrics(int id)
        {
            if (TimeMetrics.TryGetValue(id, out QueryMetrics? Metrics))
            {
                lock (_LockObject)
                {
                    _ = TimeMetrics.Remove(id);
                }
            }
            return Metrics;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="QueryListener"/> class.
        /// </summary>
        ~QueryListener()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }
    }
}