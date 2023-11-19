using DragonHoard.Core;
using Mithril.Background.Abstractions.Frequencies;
using Mithril.Background.Abstractions.Interfaces;

namespace Mithril.Caching.InMemory.BackgroundTasks
{
    /// <summary>
    /// Memory compaction scheduled task
    /// </summary>
    /// <seealso cref="IScheduledTask" />
    /// <remarks>
    /// Initializes a new instance of the <see cref="MemoryCompactionScheduledTask" /> class.
    /// </remarks>
    /// <param name="memoryCache">The memory cache.</param>
    public class MemoryCompactionScheduledTask(Cache? memoryCache) : IScheduledTask
    {
        /// <summary>
        /// Gets the frequencies that the task is run at.
        /// </summary>
        /// <value>
        /// The frequencies the task is run at.
        /// </value>
        public IFrequency[] Frequencies { get; } = [new RunEvery(TimeSpan.FromSeconds(60))];

        /// <summary>
        /// Gets the last run time.
        /// </summary>
        /// <value>
        /// The last run time.
        /// </value>
        public DateTime LastRunTime { get; set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; } = "Memory Compaction";

        /// <summary>
        /// Gets the memory cache.
        /// </summary>
        /// <value>The memory cache.</value>
        private Cache? MemoryCache { get; } = memoryCache;

        /// <summary>
        /// Executes this instance.
        /// </summary>
        /// <returns>
        /// Async task.
        /// </returns>
        public Task ExecuteAsync()
        {
            if (MemoryCache is null)
                return Task.CompletedTask;
            MemoryCache.GetOrAddCache()?.Compact(.1);
            MemoryCache.GetOrAddCache("Inflatable")?.Compact(.1);
            return Task.CompletedTask;
        }
    }
}