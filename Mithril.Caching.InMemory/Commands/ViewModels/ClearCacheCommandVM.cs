namespace Mithril.Caching.InMemory.Commands.ViewModels
{
    /// <summary>
    /// Clear cache command. Requires admin access.
    /// </summary>
    public class ClearCacheCommandVM
    {
        /// <summary>
        /// The name of the cache to clear (built in caches include "Default" for basic information
        /// and "Inflatable" for database queries).
        /// </summary>
        /// <value>
        /// The name of the cache (built in caches include "Default" for basic information and
        /// "Inflatable" for database queries).
        /// </value>
        public string? CacheName { get; set; }
    }
}