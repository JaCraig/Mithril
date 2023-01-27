using Mithril.Apm.Abstractions.Interfaces;

namespace Mithril.Apm.Abstractions.BaseClasses
{
    /// <summary>
    /// Metrics source base class
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <seealso cref="IMetricsCollector"/>
    public abstract class MetricsCollectorBaseClass<TSource> : IMetricsCollector
        where TSource : MetricsCollectorBaseClass<TSource>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MetricsSourceBaseClass"/> class.
        /// </summary>
        protected MetricsCollectorBaseClass()
        { }

        /// <summary>
        /// Finalizes an instance of the <see cref="MetricsSourceBaseClass"/> class.
        /// </summary>
        ~MetricsCollectorBaseClass()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public virtual string Name { get; } = typeof(TSource).Name;

        /// <summary>
        /// Gets the observers.
        /// </summary>
        /// <value>The observers.</value>
        private List<IObserver<MetricsEntry>> Observers { get; } = new List<IObserver<MetricsEntry>>();

        /// <summary>
        /// The disposed value
        /// </summary>
        private bool disposedValue;

        /// <summary>
        /// Adds the entry.
        /// </summary>
        /// <param name="traceId">The trace identifier.</param>
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
        /// Subscribes the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns></returns>
        public IDisposable Subscribe(IObserver<MetricsEntry> observer)
        {
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
    }
}