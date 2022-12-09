using DragonHoard.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Mithril.Caching.InMemory.HostedServices
{
    /// <summary>
    /// Memory compaction service
    /// </summary>
    /// <seealso cref="IHostedService"/>
    public class MemoryCompactionService : IHostedService, IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryCompactionService"/> class.
        /// </summary>
        /// <param name="memoryCache">The memory cache.</param>
        /// <param name="logger">The logger.</param>
        public MemoryCompactionService(Cache memoryCache, ILogger<MemoryCompactionService> logger)
        {
            MemoryCache = memoryCache;
            Logger = logger;
        }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public ILogger<MemoryCompactionService> Logger { get; }

        /// <summary>
        /// Gets the memory cache.
        /// </summary>
        /// <value>The memory cache.</value>
        public Cache MemoryCache { get; }

        /// <summary>
        /// Gets or sets the internal timer.
        /// </summary>
        /// <value>The internal timer.</value>
        private Timer? InternalTimer { get; set; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Triggered when the application host is ready to start the service.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the start process has been aborted.</param>
        /// <returns>The async task.</returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Logger.LogInformation("Starting memory compacting background service");
            InternalTimer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(60));
            return Task.CompletedTask;
        }

        /// <summary>
        /// Triggered when the application host is performing a graceful shutdown.
        /// </summary>
        /// <param name="cancellationToken">
        /// Indicates that the shutdown process should no longer be graceful.
        /// </param>
        /// <returns>The async task.</returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            Logger.LogInformation("Stopping memory compacting background service");
            InternalTimer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="managed">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release
        /// only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool managed)
        {
            if (managed)
            {
                InternalTimer?.Dispose();
                InternalTimer = null;
            }
        }

        /// <summary>
        /// Does the work.
        /// </summary>
        /// <param name="state">The state.</param>
        private void DoWork(object? state)
        {
            Logger.LogInformation("Running memory compaction");
            MemoryCache.GetOrAddCache()?.Compact(.1);
            MemoryCache.GetOrAddCache("Inflatable")?.Compact(.1);
        }
    }
}