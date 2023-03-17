using DragonHoard.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mithril.Core.Abstractions.BaseClasses;

namespace Mithril.Caching.InMemory.HostedServices
{
    /// <summary>
    /// Memory compaction service
    /// </summary>
    /// <seealso cref="IHostedService"/>
    public class MemoryCompactionService : HostedServiceBaseClass<MemoryCompactionService>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryCompactionService"/> class.
        /// </summary>
        /// <param name="memoryCache">The memory cache.</param>
        /// <param name="logger">The logger.</param>
        public MemoryCompactionService(Cache? memoryCache, ILogger<MemoryCompactionService>? logger)
            : base(logger, 60)
        {
            MemoryCache = memoryCache;
        }

        /// <summary>
        /// Gets the memory cache.
        /// </summary>
        /// <value>The memory cache.</value>
        public Cache? MemoryCache { get; }

        /// <summary>
        /// Does the work.
        /// </summary>
        /// <returns>Async task.</returns>
        protected override Task DoWorkAsync()
        {
            if (MemoryCache is null)
                return Task.CompletedTask;
            Logger?.LogInformation("Running memory compaction");
            MemoryCache.GetOrAddCache()?.Compact(.1);
            MemoryCache.GetOrAddCache("Inflatable")?.Compact(.1);
            return Task.CompletedTask;
        }
    }
}