using Mithril.Apm.Abstractions.Interfaces;

namespace Mithril.Apm.Abstractions.BaseClasses
{
    /// <summary>
    /// TraceData source base class
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <seealso cref="ITraceDataCollector"/>
    public abstract class TraceDataCollectorBaseClass<TSource> : ITraceDataCollector
        where TSource : TraceDataCollectorBaseClass<TSource>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TraceDataSourceBaseClass"/> class.
        /// </summary>
        protected TraceDataCollectorBaseClass()
        { }

        /// <summary>
        /// Finalizes an instance of the <see cref="TraceDataSourceBaseClass"/> class.
        /// </summary>
        ~TraceDataCollectorBaseClass()
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
        private List<IObserver<TraceEntry>> Observers { get; } = new List<IObserver<TraceEntry>>();

        /// <summary>
        /// The disposed value
        /// </summary>
        private bool disposedValue;

        /// <summary>
        /// Adds an entry to the collector.
        /// </summary>
        /// <param name="traceId">The trace identifier.</param>
        /// <param name="entry">The entry.</param>
        /// <returns>This.</returns>
        public ITraceDataCollector AddEntry(string traceId, KeyValuePair<string, string> entry)
        {
            try
            {
                for (var x = 0; x < Observers.Count; ++x)
                {
                    var Observer = Observers[x];
                    Observer.OnNext(new TraceEntry(this, traceId, entry));
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
        public IDisposable Subscribe(IObserver<TraceEntry> observer)
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
                Observers.Clear();
            }
        }
    }
}