using Mithril.Apm.Abstractions.Interfaces;

namespace Mithril.Apm.Abstractions.BaseClasses
{
    /// <summary>
    /// TraceData source base class
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <seealso cref="IMetaDataCollector"/>
    public abstract class MetaDataCollectorBaseClass<TSource> : IMetaDataCollector
        where TSource : MetaDataCollectorBaseClass<TSource>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MetaDataCollectorBaseClass{TSource}"/> class.
        /// </summary>
        protected MetaDataCollectorBaseClass()
        { }

        /// <summary>
        /// Finalizes an instance of the <see cref="MetaDataCollectorBaseClass{TSource}"/> class.
        /// </summary>
        ~MetaDataCollectorBaseClass()
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
        private List<IObserver<MetaDataEntry>> Observers { get; } = [];

        /// <summary>
        /// The disposed value
        /// </summary>
        private bool disposedValue;

        /// <summary>
        /// Adds an entry to the collector.
        /// </summary>
        /// <param name="traceId">The trace identifier.</param>
        /// <param name="entries">The entries.</param>
        /// <returns>This.</returns>
        public IMetaDataCollector AddEntry(string traceId, params KeyValuePair<string, string>[] entries)
        {
            try
            {
                for (var x = 0; x < Observers.Count; ++x)
                {
                    IObserver<MetaDataEntry> Observer = Observers[x];
                    Observer.OnNext(new MetaDataEntry(this, traceId, entries));
                }
            }
            catch (Exception ex)
            {
                for (var x = 0; x < Observers.Count; ++x)
                {
                    IObserver<MetaDataEntry> Observer = Observers[x];
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
        public IDisposable Subscribe(IObserver<MetaDataEntry> observer)
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